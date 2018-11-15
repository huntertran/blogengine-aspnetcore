<template>
  <v-container fluid grid-list-md>
    <v-layout>
      <v-flex xs12>
        <v-toolbar>
          <v-spacer></v-spacer>
          <v-toolbar-items class="hidden-sm-and-down">
            <v-btn flat :key="category.id" v-for="category in categories">{{category.name}}</v-btn>
          </v-toolbar-items>
        </v-toolbar>
      </v-flex>
    </v-layout>
    <v-layout row wrap>
      <v-flex xs12 sm4 v-for="post in posts" :key="post.id">
        <v-card>
          <v-img
          src="https://cdn.vuetifyjs.com/images/cards/desert.jpg"
          aspect-ratio="2.75"
          ></v-img>

          <v-card-title primary-title>
          <div>
            <h3 class="headline mb-0">{{post.title}}</h3>
            <div>{{post.summary}}</div>
          </div>
          </v-card-title>

          <v-card-actions>
            <v-btn flat color="orange">Read</v-btn>
          </v-card-actions>
        </v-card>
      </v-flex>
    </v-layout>
  </v-container>
</template>

<script>
import Axios from "axios";

export default {
  data: function() {
    return {
      categories: [],
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
    }
  },
  mounted() {
    this.getCategories();
    this.getPosts(this.page);
  }
};
</script>
