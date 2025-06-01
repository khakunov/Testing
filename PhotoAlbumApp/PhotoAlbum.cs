using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

public class PhotoAlbum
{
    public List<Photo> photos = new List<Photo>();
    public ListView listView;
    public PhotoAlbum(ListView listView)
    {
        this.listView = listView;
        LoadPhotos();
    }
    public void LoadPhotos()
    {
        listView.Items.Clear();
        foreach (var photo in photos)
        {
            listView.Items.Add(new ListViewItem(new[] { photo.Path, photo.Description, photo.DateTaken.ToString("dd.MM.yyyy") }));
        }
    }
    public void AddPhoto()
    {
        using (var openFileDialog = new OpenFileDialog())
        {
            openFileDialog.InitialDirectory =
            Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            openFileDialog.Title = "Выберите фото";
            openFileDialog.Filter = "Изображения (*.jpg;*.png;*.jpeg)|*.jpg;*.png;*.jpeg";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                var photoPath = openFileDialog.FileName;
                var description = GetDescription();
                var dateTaken = DateTime.ParseExact(description, "dd.MM.yyyy", null);
                photos.Add(new Photo(photoPath, description, dateTaken));
                LoadPhotos();
                MessageBox.Show("Фото добавлено.");
            }
        }
    }
    public void RemovePhoto()
    {
        if (listView.SelectedItems.Count == 0)
        {
            MessageBox.Show("Сначала выберите фото для удаления.");
            return;
        }
        var photoPath = listView.SelectedItems[0].SubItems[0].Text;
        photos.RemoveAll(p => p.Path == photoPath);
        LoadPhotos();
        MessageBox.Show("Фото удалено.");
    }
    public void SortPhotosByDate()
    {
        var sortedPhotos = photos.OrderBy(p => p.DateTaken).ToList();
        photos = new List<Photo>(sortedPhotos);
        LoadPhotos();
        MessageBox.Show("Фото отсортированы по дате.");
    }
    public string GetDescription()
    {
        return $"";
    }
}
