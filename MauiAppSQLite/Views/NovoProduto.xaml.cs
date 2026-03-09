using MauiAppSQLite.Models;

namespace MauiAppSQLite.Views;

public partial class NovoProduto : ContentPage
{
	public NovoProduto()
	{
		InitializeComponent();
	}

    private async void ToolbarItem_Clicked(object sender, EventArgs e)
    {
		try
		{
			Product p = new Product
			{
				Name = txt_nome.Text,
				Quantity = Convert.ToInt16(txt_quantidade.Text),
				Price = Convert.ToDecimal(txt_preco.Text)
			};

			await App.Database.Insert(p);
			await DisplayAlert("Sucesso", "Registro inserido", "ok");
				
		}catch(Exception ex)
		{
			await DisplayAlert("Ops", ex.Message, "Ok");
		}
    }
}