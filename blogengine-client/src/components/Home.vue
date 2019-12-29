<template>
    <v-container fluid grid-list-md>
      <v-layout v-if="category.isShow" row wrap>
        <h1>{{category.name}}</h1>
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
              <v-btn text color="orange" :to="readPost(post.id)">Read</v-btn>
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
      posts: [],
      query: {
        page: 1,
        postPerPage: 1,
        categoryId: 1
      },
      category: {
        isShow: false,
        name: ""
      }
    };
  },
  methods: {
    getCategories: function() {
      var _this = this;
      Axios.get("/categories/all").then(function(response) {
        _this.categories = response.data;
      });
    },
    getSingleCategory: function(categoryId) {
      var _this = this;
      Axios.get("/categories/find?id=" + categoryId).then(function(response) {
        _this.category.isShow = true;
        _this.category.name = response.data.name;
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
    this.query.page = this.$route.query.page;
    this.query.postPerPage = this.$route.query.postPerPage;
    this.query.categoryId = this.$route.query.categoryId;
    if (this.query.categoryId === 0 || isNaN(this.query.categoryId)) {
      this.category.isShow = false;
      this.query.categoryId = 0;
    } else {
      this.getSingleCategory(this.query.categoryId);
    }

    if (isNaN(this.query.postPerPage)) {
      this.query.postPerPage = 5;
    }

    if (isNaN(this.query.page)) {
      this.query.page = 1;
    }

    this.getPosts(
      this.query.page,
      this.query.postPerPage,
      this.query.categoryId
    );
  }
};
</script>
