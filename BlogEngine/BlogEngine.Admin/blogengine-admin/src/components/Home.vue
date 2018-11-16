<template>
  <div>
    <!-- <v-toolbar>
      <v-spacer></v-spacer>
      <v-toolbar-items class="hidden-sm-and-down">
        <v-btn flat :key="category.id" v-for="category in categories">{{category.name}}</v-btn>
      </v-toolbar-items>
    </v-toolbar> -->
    <v-container fluid grid-list-md>
      <v-layout row wrap>
        <v-flex xs12 sm4 v-for="post in posts" :key="post.id">
          <v-card hover>
            <v-img
              src="https://cdn.vuetifyjs.com/images/cards/desert.jpg"
              aspect-ratio="2.75"
            ></v-img>

            <v-card-title primary-title>
              <div>
                <h3 class="headline mb-0">{{ post.title }}</h3>
                <div>{{ post.summary }}</div>
              </div>
            </v-card-title>

            <v-card-actions>
              <v-spacer></v-spacer>
              <v-btn flat color="orange" :to="readPost(post.id)">Read</v-btn>
            </v-card-actions>
          </v-card>
        </v-flex>
      </v-layout>
    </v-container>
  </div>
</template>

<script>
import Axios from "axios";

export default {
  data: function() {
    return {
      posts: [],
      page: 1
    };
  },
  methods: {
    getCategories: function() {
      var _this = this;
      Axios.get("/categories/all").then(function(response) {
        _this.categories = response.data;
      });
    },
    getPosts: function(page) {
      var _this = this;
      Axios.get("/posts/all?page=" + page).then(function(response) {
        _this.posts = response.data;
      });
    },
    readPost: function(postId) {
      // var _this = this;
      // _this.$router.push("/post/" + postId);
      return "/post/" + postId;
    }
  },
  mounted() {
    this.getCategories();
    this.getPosts(this.page);
  }
};
</script>
