using MauiAppSQLite.Models;

namespace MauiAppSQLite.Views;

public partial class EditaProduto : ContentPage
{
	public EditaProduto()
	{
		InitializeComponent();
	}

    private async void ToolbarItem_Clicked(object sender, EventArgs e)
    {
		try
		{
			Product p_fixed = BindingContext as Product;

			Product p = new Product
			{
				Id= p_fixed.Id,
                Name = txt_nome.Text,
				Quantity = int.Parse(txt_quantidade.Text),
				Price = decimal.Parse(txt_preco.Text),
				Category = txt_categoria.SelectedItem.ToString(),
            };
			await App.Database.Update(p);
			await DisplayAlertAsync("Sucesso", "Produto atualizado com sucesso!", "Ok");
			await Navigation.PopAsync();
        }
		catch (Exception ex)
		{
			await DisplayAlertAsync("Ops", ex.Message, "Ok");
        }

    }
}