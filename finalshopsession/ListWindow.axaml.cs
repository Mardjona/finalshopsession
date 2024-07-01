using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System.Linq;
using System;
using System.Collections.Generic;

using System.IO;
using Avalonia.Input;
using static finalshopsession.Stats;

namespace finalshopsession;

public partial class ListWindow : Window
{
    private List<string> _Suppliers = []; 
    private string[] _Price = ["По умолчанию", "По убыванию", "Повозрастанию"];
    public ListWindow()
    {
        InitializeComponent();
        btn_addProduct.IsVisible = _UserAutorized.isAdmin; 
        btn_toShoppingCart.IsVisible = !_UserAutorized.isGuest; 
        LBoxInitialization(_LboxItems); 

        tblock_user.Text += _UserAutorized.isAdmin == false ? (_UserAutorized.isGuest == true ? "Гость" : "Юзер") : "Админ"; 
        CartCountVisibility();

        SupplierUpdate();
        tbox_search.Text = _SearchingText; 
        ComboBoxInit(); 
    }

    private void LBoxInitialization(List<Product> listBoxSource) 
    {
        LBox.ItemsSource = listBoxSource.Select(x => new 
        {
            x.pName,
            x.pId,
            x.pDescription,
            x.pSupplier,
           // x.pImage,
            x.pPrice,
            x.pQuantity,
            Measurement = " " + _Measurements[x.pMeasurement],
            Admin = _UserAutorized.isAdmin, 
            Guest = _UserAutorized.isGuest, 
           
        });
    }

    private void SupplierUpdate() 
    {
        _Suppliers.Add("По умолчанию");
        foreach (Product product in _LboxItems) 
        {
            if (!_Suppliers.Contains(product.pSupplier)) 
                _Suppliers.Add(product.pSupplier); 
        }
    }

    private void Edit(object? sender, PointerReleasedEventArgs pointerReleasedEventArgs)
    {
        if (_UserAutorized.isAdmin)
        {
            _SelectedProduct = _LboxItems[_SelectedInex_Supplier!];
            OpenRedWindow();
        }


    }

    private void ComboBoxInit() 
    {
        cbox_sortSuppliers.ItemsSource = _Suppliers; 
        cbox_sortSuppliers.SelectedIndex = _SelectedInex_Supplier;
        cbox_sortPrice.ItemsSource = _Price; 
        cbox_sortPrice.SelectedIndex = _SelectedInex_Price;
    }

    private List<CartItem> UsersCartCheck() 
    {
        foreach (User user in _Users) 
        {
            if (user.UserCart.Count > 0) 
                return user.UserCart; 
        }
        return null; 
    }

  
    private void OpenRedWindow() 
    {
        RedWindow redWindow = new();
        redWindow.Show();
        this.Close();
    }

   
    private void LogOut(object? sender, Avalonia.Interactivity.RoutedEventArgs e) 
    {
        _UserAutorized = null; 
        _SearchingText = ""; 
        _SelectedInex_Price = _SelectedInex_Supplier = 0; 
        MainWindow mainWindow = new();
        mainWindow.Show();
        this.Close();
    }

    private void ToCart(object? sender, Avalonia.Interactivity.RoutedEventArgs e) 
    {
        CartWindow cartWindow = new();
        cartWindow.Show();
        this.Close();
    }

   

    private void ProductManipulation(object? sender, Avalonia.Interactivity.RoutedEventArgs e)  
    {
        if (UsersCartCheck() == null) 
        {
            var button = (sender as Button)!;
            switch (button.Name)
            {
                case "btn_addProduct": 
                    {
                        OpenRedWindow();
                    }
                    break;
              
                  
                case "btn_del": 
                    {
                        if (_LboxItems[(int)button!.Tag!].pImageSource != null) 
                        {
                            string? imgDel = _LboxItems[(int)button!.Tag!].pImageSource; 
                            _LboxItems[(int)button!.Tag!].pImageSource = null; 
                            File.Delete($"Assets/{imgDel}"); 
                        }
                        _LboxItems.RemoveAt((int)button!.Tag!);
                        for (int i = 0; i < _LboxItems.Count; i++) 
                        {
                            _LboxItems[i].pId = i;
                        }
                        SearchingAndSorting(); 
                    }
                    break;
            }
        }
    }

  

    private void SearchingActivity(object? sender, KeyEventArgs e) 
    {
        _SearchingText = tbox_search.Text; 
        SearchingAndSorting(); 
    }

    private void SelectionChanging(object? sender, SelectionChangedEventArgs e) 
    {
        var combobox = (sender as ComboBox)!;
        switch (combobox.Name)
        {
            case "cbox_sortSuppliers":
                _SelectedInex_Supplier = combobox.SelectedIndex;
                break;
            case "cbox_sortPrice": 
                _SelectedInex_Price = combobox.SelectedIndex;
                break;
        }
        SearchingAndSorting(); 
    }

    private void SuppliersSorting() 
    {
        _FoundedProducts.Clear(); 
        if (cbox_sortSuppliers.SelectedIndex != 0 && cbox_sortSuppliers.SelectedIndex != -1) 
        {
            foreach (Product product in _LboxItems) 
            {
                if (product.pSupplier == _Suppliers[cbox_sortSuppliers.SelectedIndex]) 
                {
                    _FoundedProducts.Add(product); 
                }
            }
        }
        LBoxInitialization(cbox_sortSuppliers.SelectedIndex == 0 ? _LboxItems : _FoundedProducts);
    }

    private void Searching() 
    {
        List<Product> searching = []; 
        searching.AddRange(cbox_sortSuppliers.SelectedIndex == 0 ? _LboxItems : _FoundedProducts);
        if (tbox_search.Text != "") 
        {
            _FoundedProducts.Clear(); 
            string[] keywords = tbox_search.Text.Split();
            foreach (Product product in searching) 
            {
                if (product.pName.Trim().ToLower().Contains(keywords[0].Trim().ToLower())) 
                {
                    if (keywords.Length == 1) 
                    {
                        _FoundedProducts.Add(product); 
                    }
                    else if (product.pDescription.Trim().ToLower().Contains(keywords[1].Trim().ToLower())) 
                    {
                        if (keywords.Length == 2) 
                        {
                            _FoundedProducts.Add(product);
                        }
                        else if (product.pSupplier.Trim().ToLower().Contains(keywords[2].Trim().ToLower())) 
                        {
                            if (keywords.Length == 3) 
                            {
                                _FoundedProducts.Add(product); 
                            }
                            else if (Convert.ToString(product.pPrice).Trim().ToLower().Contains(keywords[3].Trim().ToLower())) 
                            {
                                if (keywords.Length == 4) 
                                {
                                    _FoundedProducts.Add(product); 
                                }
                                else if (Convert.ToString(product.pQuantity).Trim().ToLower().Contains(keywords[4].Trim().ToLower())) 
                                {
                                    if (keywords.Length == 5) 
                                    {
                                        _FoundedProducts.Add(product); 
                                    }
                                    else if (_Measurements[product.pMeasurement].Trim().ToLower().Contains(keywords[5].Trim().ToLower())) 
                                    {
                                        if (keywords.Length == 6) 
                                            _FoundedProducts.Add(product);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            LBoxInitialization(_FoundedProducts);
        }
        else
        {
            LBoxInitialization(searching);
        }
    }

    private void FoundInfo() 
    {
        if (cbox_sortSuppliers.SelectedIndex != 0 || tbox_search.Text != "") 
        {
           
            tblock_searchCount.IsVisible = true; 
        }
        else
        {
            tblock_searchCount.IsVisible = false;
        }
    }

    private void SearchingAndSorting() 
    {
        SuppliersSorting(); 
        Searching(); 
        BubbleSorting(cbox_sortPrice.SelectedIndex); 
        FoundInfo(); 
    }

    private void BubbleSorting(int selectedOption) 
    {
        List<Product> bubble = []; 
        bubble.AddRange(cbox_sortSuppliers.SelectedIndex != 0 || tbox_search.Text != "" ? _FoundedProducts : _LboxItems);  
        switch (selectedOption)
        {
            case 1: 
                {
                    for (int i = 0; i < bubble.Count; i++)
                        for (int j = 0; j < bubble.Count - i - 1; j++)
                        {
                            if (bubble[j].pPrice > bubble[j + 1].pPrice)
                            {
                                (bubble[j], bubble[j + 1]) = (bubble[j + 1], bubble[j]);
                            }
                        }
                }
                break;
            case 2:
                {
                    for (int i = 0; i < bubble.Count; i++)
                        for (int j = 0; j < bubble.Count - i - 1; j++)
                        {
                            if (bubble[j].pPrice < bubble[j + 1].pPrice)
                            {
                                (bubble[j], bubble[j + 1]) = (bubble[j + 1], bubble[j]);
                            }
                        }
                }
                break;
        }
        LBoxInitialization(bubble);
    }

   

    private void CartCountVisibility() 
    {
       
        tblock_cartCount.IsVisible = _UserAutorized.UserCart.Count > 0 ? true : false; 
    }

    private void AddToCart(Product product) 
    {
        for (int i = 0; i < _UserAutorized.UserCart.Count; i++) 
        {
            if (_UserAutorized.UserCart[i].prId == product.pId) 
            {
                _UserAutorized.UserCart[i].cQuantity++; 
                product.pQuantity--; 
                _UserAutorized.UserCart[i].cPrice = _UserAutorized.UserCart[i].cPrice + product.pPrice; 
                return; 
            }
        }
        _UserAutorized.UserCart.Add(new CartItem(_UserAutorized.UserCart.Count, product.pId, product.pName, product.pPrice, 1)); 
        product.pQuantity--; 
    }

    private void RemoveFromCart(Product product) 
    {
        for (int i = 0; i < _UserAutorized.UserCart.Count; i++) 
        {
            if (_UserAutorized.UserCart[i].prId == product.pId)  
            {
                _UserAutorized.UserCart[i].cQuantity--; 
                _UserAutorized.UserCart[i].cPrice = _UserAutorized.UserCart[i].cPrice - product.pPrice; 
                product.pQuantity++; 
                if (_UserAutorized.UserCart[i].cQuantity == 0) 
                {
                    _UserAutorized.UserCart.RemoveAt(i);
                    for (int j = 0; j < _UserAutorized.UserCart.Count; j++) 
                    {
                        _UserAutorized.UserCart[j].cId = j;
                    }
                }
                return; 
            }
        }
    }

    private void CartManipulation(object? sender, Avalonia.Interactivity.RoutedEventArgs e) 
    {
        var button = (sender as Button)!;
        switch (button.Name)
        {
            case "btn_cartAdd": 
                {
                    if (_LboxItems[(int)button!.Tag!].pQuantity > 0) 
                    {
                        AddToCart(_LboxItems[(int)button!.Tag!]); 
                    }
                }
                break;
            case "btn_cartDel": 
                {
                    if (_UserAutorized.UserCart.Count > 0) 
                    {
                        RemoveFromCart(_LboxItems[(int)button!.Tag!]); 
                    }
                }
                break;
        }
        SearchingAndSorting(); 
        CartCountVisibility(); 
    }

    
}