﻿using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace XamUDemy.DataAccess
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
    }

    public partial class ConsumingRESTful : ContentPage
    {
        private const string Url = "https://jsonplaceholder.typicode.com/posts";
        //To consumer RESTful (web) services we need to create a new INSTANCE of HttpClient 
        private HttpClient _client = new HttpClient();

        private ObservableCollection<Post> _posts;

		public ConsumingRESTful()
		{
			InitializeComponent();
		}

        protected override async void OnAppearing()
        {
            //Here we are sending a GET request to this endpoint to get the LIST of ALL POSTS
            var content= await _client.GetStringAsync(Url);

            //Deserialize the string
            var posts = JsonConvert.DeserializeObject<List<Post>>(content);

            //Intialize ObservableCollection
            _posts = new ObservableCollection<Post>(posts);
            postsListView.ItemsSource = _posts;

            base.OnAppearing();
        }

        void OnDelete(object sender, System.EventArgs e)
        {
            var post = _posts[0];

            //This is removing the entry from the listView
            _posts.Remove(post);

            _client.DeleteAsync(Url + "/" + post.Id );

            //Don't need to serialize because this post is not going to the body (listview) 
        }

        async void OnUpdate(object sender, System.EventArgs e)
        {
            var post = _posts[0];
            post.Title += "UPDATED";

			var content = JsonConvert.SerializeObject(post);
            await _client.PutAsync(Url + "/" + post.Id, new StringContent(content));
        }

        async void OnAdd(object sender, System.EventArgs e)
        {
            var post = new Post { Title = "Title" + DateTime.Now.Ticks };

			//This is how you add to postsListView
			//We are Inserting an Index to ensure the new posts are on top first
			//The placement here is KEY
			//This gets executed first so the list is updated IMMEDIATELY then the server is updated
			_posts.Insert(0, post);

            //This is pusing the changes to the server
            var content = JsonConvert.SerializeObject(post);
            await _client.PostAsync(Url, new StringContent(content));

        }

    }
}
