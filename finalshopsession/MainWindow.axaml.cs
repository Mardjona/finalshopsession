using Avalonia.Controls;
using static finalshopsession.Stats;
namespace finalshopsession;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }
     public void LoggingIn(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            var button = (sender as Button)!;
            switch (button.Name)
            {
                case "btn_login":
                    {
                       tblock_password.IsVisible = false;
                        tblock_password.IsVisible = false;
                        if (tbox_login.Text != null || tbox_login.Text != "" || tbox_password.Text != null || tbox_password.Text != "") 
                        {
                            foreach (User user in _Users)
                            {
                                if (user.uName == tbox_login.Text) 
                                {
                                    tblock_login.IsVisible = false; 
                                    if (user.uPassword == tbox_password.Text) //���� ��������� ������ ��������� � ������� ���������� ������������
                                    {
                                        _UserAutorized = user; //���������������� ������������ ���������� �������� �������������
                                        ListWindow listWindow = new(); //������� � ���� ������ �������
                                        listWindow.Show();
                                        this.Close();
                                    }
                                    else
                                    {
                                        tblock_password.IsVisible = true; 
                                        break;
                                    }
                                }
                                else
                                {
                                    tblock_login.IsVisible = true; 
                                    tblock_password.IsVisible = true;
                                }
                            }
                        }
                    }
                    break;
                case "btn_guest": 
                    {
                        _UserAutorized = new("guest", "", false, true); 
                        ListWindow listWindow = new();
                        listWindow.Show();
                        this.Close();
                    }
                    break;
            }
        }
    }
