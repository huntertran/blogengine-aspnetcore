<template>
    <v-container fluid grid-list-md>
        <v-layout row wrap>
            <h1>Machine Learning</h1>
        </v-layout>
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
</template>

<script>
import Axios from "axios";

export default {
  data: function() {
    return {
      posts: []
    };
  },
  props: {
    page: {
      type: Number,
      default: 1
    },
    postPerPage: {
      type: Number,
      default: 5
    },
    categoryId: {
      type: Number,
      default: 0
    }
  },
  methods: {
    getCategories: function() {
      var _this = this;
      Axios.get("/categories/all").then(function(response) {
        _this.categories = response.data;
      });
    },
    getPosts: function(page, postPerPage, categoryId) {
      var _this = this;
      Axios.get(
        "/posts/all?page=" +
          page +
          "&postPerPage=" +
          postPerPage +
          "&categoryId=" +
          categoryId
      ).then(function(response) {
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
    this.getPosts(this.page, this.postPerPage, this.categoryId);
  }
};
</script>
