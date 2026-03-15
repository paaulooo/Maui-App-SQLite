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
        List<Product> tmp = await App.Database.GetAll();

        tmp.ForEach(i => list.Add(i));
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
        string q = e.NewTextValue;

        list.Clear();

        List<Product> tmp = await App.Database.Search(q);
        tmp.ForEach(i =>
        {
            if (!list.Contains(i))
                list.Add(i);
        });
    }

    private async void MenuItem_Clicked(object sender, EventArgs e)
    {
        MenuItem item = sender as MenuItem;
        var p = item?.CommandParameter as Product;

        if (p != null)
        {
            await App.Database.Delete(p.Id);
            list.Clear();

            List<Product> tmp = await App.Database.GetAll();

            tmp.ForEach(i => list.Add(i));
        }
    }
}