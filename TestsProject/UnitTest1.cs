using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows.Forms;
using PhotoAlbumApp;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace TestsProject
{
    [TestClass]
    public class UnitTest1
    {
        private ListView listView;
        private PhotoAlbum album;

        [TestInitialize]
        public void TestInitialize()
        {
            listView = new ListView();
            album = new PhotoAlbum(listView);
        }

        [TestMethod]
        public void PhotoTest_ShouldCreatePhotoInstance()
        {
            string path = "filePath";
            string description = "some description";
            DateTime dateTime = DateTime.Now;

            Photo photo = new Photo(path, description, dateTime);

            Assert.IsNotNull(photo);
            Assert.AreEqual("filePath", photo.Path);
            Assert.AreEqual("some description", photo.Description);
            Assert.AreEqual(dateTime, photo.DateTaken);
        }

        [TestMethod]
        public void PhotoToString_ShoulReturnFullInfo()
        {
            string path = "filePath";
            string description = "some description";
            DateTime dateTime = DateTime.Now;

            Photo photo = new Photo(path, description, dateTime);

            Assert.AreEqual("filePath - some description (01.06.2025)", photo.ToString());
        }

        [TestMethod]
        public void RemovePhoto_RemovesSelectedPhoto()
        {
            album.photos.Add(new Photo("test1.jpg", "desc", DateTime.Now));
            album.photos.Add(new Photo("test2.jpg", "desc", DateTime.Now));
            album.LoadPhotos();

            listView.Items[0].Selected = true;
            listView.Items[0].Focused = true;

            album.RemovePhoto();

            Assert.AreEqual(1, album.photos.Count);
            Assert.AreEqual("test2.jpg", album.photos[0].Path);
        }

        [TestMethod]
        public void SortPhotosByDate_SortsCorrectly()
        {
            album.photos.Add(new Photo("1.jpg", "desc", DateTime.Now.AddDays(2)));
            album.photos.Add(new Photo("2.jpg", "desc", DateTime.Now));
            album.photos.Add(new Photo("3.jpg", "desc", DateTime.Now.AddDays(1)));

            album.SortPhotosByDate();

            Assert.IsTrue(album.photos[0].DateTaken < album.photos[1].DateTaken);
            Assert.IsTrue(album.photos[1].DateTaken < album.photos[2].DateTaken);
        }
    }
}
