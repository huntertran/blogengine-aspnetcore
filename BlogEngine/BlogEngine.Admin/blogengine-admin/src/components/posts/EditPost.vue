<template>
  <v-container>
    <v-layout row wrap>
      <v-flex md8 xs12>
        <v-form>
          <h2>Edit Post</h2>
          <v-text-field
            v-model="post.title"
            label="Post Title"
            required
          ></v-text-field>

          <v-text-field
            v-model="post.summary"
            label="Post Summary"
            required
          ></v-text-field>
          <editor
            api-key="lb0mhaz5el6xk5icuw5ohvya2g0o4on9yj6lqm4bw6zsnl3e"
            v-model="post.content"
          ></editor>
        </v-form>
      </v-flex>
      <v-flex md4 xs12>
        <v-form>
          <h2>Category</h2>
          <v-checkbox v-for="item in postCategoryList" 
            :key="item.name"
            v-model="item.isSelected"
            :label="item.name"
          ></v-checkbox>
        </v-form>
      </v-flex>
    </v-layout>
    <v-layout row wrap>
      <v-flex xs12>
          <v-btn color="success" v-on:click="save">Save</v-btn>
          <v-btn v-if="post.isPublished" color="info" v-on:click="unpublish">Un Publish</v-btn>
          <v-btn v-else color="info" v-on:click="publish">Publish</v-btn>
      </v-flex>
    </v-layout>
  </v-container>
</template>

<script>
import Axios from "axios";

import Editor from "@tinymce/tinymce-vue";

export default {
  props: ["id"],
  data: function() {
    return {
      post: {
        title: "",
        summary: "",
        content: "",
        isPublished: false
      },
      postCategoryList: []
    };
  },
  components: {
    editor: Editor
  },
  methods: {
    save: function() {
      var _this = this;
      Axios.put("/posts/put/", _this.post).then(function(response) {
        if (response.status === 200 || response.status === 201) {
          var post = response.data;
          _this.saveCategory(post.id);
          // _this.$router.push("/posts");
        }
      });
    },
    saveCategory: function(postId) {
      var _this = this;
      Axios.post(
        "/posts/PostCategoryOfPost?postId=" + postId,
        _this.postCategoryList
      ).then(function(response) {
        if (response.status === 200 || response.status === 201) {
          _this.$router.push("/posts");
        }
      });
    },
    publish: function() {
      var _this = this;
      _this.post.isPublished = true;
      _this.submit();
    },
    unpublish: function() {
      var _this = this;
      _this.post.isPublished = false;
      _this.submit();
    },
    getPost: function(id) {
      var _this = this;
      Axios.get("/posts/find?id=" + id).then(function(response) {
        _this.post = response.data;
      });
    },
    getPostCategoryList: function(id) {
      var _this = this;
      Axios.get("/posts/getcategoryofpost?postId=" + id).then(function(
        response
      ) {
        _this.postCategoryList = response.data;
      });
    }
  },
  mounted: function() {
    this.getPost(this.id);
    this.getPostCategoryList(this.id);
  }
};
</script>
