using System.Text;

namespace bloc_de_notas_app.Views;
[QueryProperty(nameof(ItemId), nameof(ItemId))]

public partial class NotePage : ContentPage
{

	public string ItemId { set { LoadNote(value); } }

	public NotePage()
	{
		InitializeComponent();
		ModifyEntry();
		Animate();

		string appDataPath = FileSystem.AppDataDirectory;
		string randomFile = $"{Path.GetRandomFileName()}.notes.txt";

		LoadNote(Path.Combine(appDataPath, randomFile));

	}

	public async void Animate()
	{
		await TextEditor.TranslateTo(0, 100, 0);
		await Task.WhenAll(
			TextEditor.TranslateTo(0, -100, 500),
            TextEditor.FadeTo(1, 500),
            gridView.TranslateTo(0, -100, 500)
			);
    }

	private void LoadNote(string fileName)
	{
		Models.Note note = new Models.Note();
		note.FileName = fileName;

		if (File.Exists(fileName))
		{
			note.Date = File.GetCreationTime(fileName);
			note.Text = File.ReadAllText(fileName);
		}

		BindingContext = note;
	}

    private async void SaveNote_Clicked(object sender, EventArgs e)
    {
		if(BindingContext is Models.Note note)
			File.WriteAllText(note.FileName, TextEditor.Text);

        await Shell.Current.GoToAsync("..");
		await DisplayAlert("Nota guardada", "", "Ok");
    }

    private async void DeleteNote_Clicked(object sender, EventArgs e)
    {
        
		var res = await DisplayAlert("Seguro?", "Seguro de que quieres eliminar esta nota", "Si", "No");

		if (res)
		{
            if (BindingContext is Models.Note note)
            {
                if (File.Exists(note.FileName))
                    File.Delete(note.FileName);
            }

            await Shell.Current.GoToAsync("..");
            await DisplayAlert("Nota Eliminada", "", "Ok");
        }
		
    }

    void ModifyEntry()
    {
        Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping("MyCustomization", (handler, view) =>
		{
			if (view is Entry) 
			{
				#if ANDROID
							handler.PlatformView.SetPadding(20, 20, 20, 20);
				#endif
			}
        });
    }

}