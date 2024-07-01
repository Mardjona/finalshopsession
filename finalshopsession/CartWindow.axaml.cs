using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System;
using static finalshopsession.Stats;

namespace finalshopsession;

public partial class CartWindow : Window
{
    private double _wholePrice = 0; 
    public CartWindow()
    {
        InitializeComponent();
        LBox_cart.ItemsSource = _UserAutorized.UserCart.ToArray(); 
        SetPrice(); 
    }

    private void SetPrice() 
    {
        for (int i = 0; i < _UserAutorized.UserCart.Count; i++) 
        {
            _wholePrice += _UserAutorized.UserCart[i].cPrice; 
        }
        _wholePrice = _UserAutorized.UserCart.Count > 0 ? _wholePrice : 0; 
        tblock_price.Text = Convert.ToString(_wholePrice); 
    }

    private void CartActivity(object? sender, Avalonia.Interactivity.RoutedEventArgs e) 
    {
        var button = (sender as Button)!;
        switch (button.Name)
        {
            case "btn_cartReturn": 
                {
                    ListWindow listWindow = new();
                    listWindow.Show();
                    this.Close();
                }
                break;
            case "btn_cartClear": 
                {
                    for (int i = 0; i < _LboxItems.Count; i++)
                    {
                        for (int j = 0; j < _UserAutorized.UserCart.Count; j++) 
                        {
                            if (_LboxItems[i].pId == _UserAutorized.UserCart[j].prId) 
                            {
                                _LboxItems[i].pQuantity += _UserAutorized.UserCart[j].cQuantity; 
                            }
                        }
                    }
                    _UserAutorized.UserCart.Clear(); 
                    SetPrice(); 
                    LBox_cart.ItemsSource = _UserAutorized.UserCart.ToArray(); 
                }
                break;
            case "btn_cartItemDelete": 
                {
                    for (int i = 0; i < _LboxItems.Count; i++) 
                    {
                        if (_LboxItems[i].pId == _UserAutorized.UserCart[(int)button!.Tag!].prId) 
                        {
                            _LboxItems[i].pQuantity += _UserAutorized.UserCart[(int)button!.Tag!].cQuantity;
                        }
                    }
                    _UserAutorized.UserCart.RemoveAt((int)button!.Tag!);
                    for (int j = 0; j < _UserAutorized.UserCart.Count; j++)
                    {
                        _UserAutorized.UserCart[j].cId = j;
                    }
                    LBox_cart.ItemsSource = _UserAutorized.UserCart.ToArray();
                    SetPrice();
                }
                break;
        }
    }
}