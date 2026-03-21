using MauiAppSQLite.Models;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Runtime;

namespace MauiAppSQLite.Views;

public partial class ListaProduto : ContentPage
{
    ObservableCollection<Product> list = new ObservableCollection<Product>();
    public ListaProduto()
    {
        InitializeComponent();
        lst_produto.ItemsSource = list;
    }

    protected async override void OnAppearing()
    {
        try
        {
            List<Product> tmp = await App.Database.GetAll();

            tmp.ForEach(i => list.Add(i));
        }
        catch (Exception ex)
        {
            await DisplayAlertAsync("Ops", ex.Message, "Ok");

        }

    }

    private void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            list.Clear();
            Navigation.PushAsync(new Views.NovoProduto());
        }
        catch (Exception ex)
        {
            DisplayAlertAsync("Ops", ex.Message, "Ok");
        }
    }

    private void ToolbarItem_Clicked_1(object sender, EventArgs e)
    {
        decimal total = list.Sum(i => i.TotalPrice);

        string msg = $"O valor total dos produtos é {total.ToString("C", CultureInfo.CurrentCulture)}";

        DisplayAlertAsync("Total", msg, "Ok");
    }

    private async void txt_search_TextChanged(object sender, TextChangedEventArgs e)
    {
        try
        {

            string q = e.NewTextValue;

            lst_produto.IsRefreshing = true;

            list.Clear();

            List<Product> tmp = await App.Database.Search(q);
            tmp.ForEach(i =>
            {
                if (!list.Contains(i))
                    list.Add(i);
            });
        }
        catch (Exception ex) 
        {
            await DisplayAlertAsync("Ops", ex.Message, "Ok");
        }
        finally
        {
            lst_produto.IsRefreshing = false;
        }
    }

    private async void MenuItem_Clicked(object sender, EventArgs e)
    {

        try
        {
            MenuItem item = sender as MenuItem;
            Product p = item?.BindingContext as Product;

            bool confirm = await DisplayAlertAsync($"Tem certeza que quer excluir {p.Name}?", "Remover", "Sim", "Não");

            if (confirm)
            {
                if (p != null)
                {
                    await App.Database.Delete(p.Id);
                    list.Clear();

                    List<Product> tmp = await App.Database.GetAll();

                    tmp.ForEach(i => list.Add(i));
                }
            }
        }
        catch (Exception ex)
        {
            await DisplayAlertAsync("Ops", ex.Message, "Ok");
        }
        
    }

    private async void lst_produto_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        try
        {
            Product p = e.SelectedItem as Product;
            list.Clear();
            
            await Navigation.PushAsync(new Views.EditaProduto { BindingContext = p});
        }
        catch (Exception ex)
        {
            await DisplayAlertAsync("Ops", ex.Message, "Ok");
        }
    }

    private async void lst_produto_Refreshing(object sender, EventArgs e)
    {
        try
        {
            list.Clear();
            List<Product> tmp = await App.Database.GetAll();

            tmp.ForEach(i => list.Add(i));
        }
        catch(Exception ex)
        {
            await DisplayAlertAsync("Ops", ex.Message, "Ok");
        }
        finally
        {
            lst_produto.IsRefreshing = false;
        }
    }
}