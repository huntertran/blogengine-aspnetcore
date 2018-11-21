<template>
    <v-container>
      <v-layout>
        <h1>{{post.title}}</h1>
      </v-layout>
        <v-layout>
            <v-flex xs12 v-html="post.content"></v-flex>
        </v-layout>
    </v-container>
</template>

<script>
import Axios from "axios";

export default {
  data: function() {
    return {
      post: {
        title: "",
        summary: "",
        content: ""
      }
    };
  },
  methods: {
    getPost: function(postId) {
      var _this = this;
      Axios.get("/posts/find?id=" + postId).then(function(response) {
        _this.post = response.data;
      });
    }
  },
  mounted() {
    this.getPost(this.$route.params.postId);
    // registerBrush(CSharpBrush);
    // SyntaxHighlighter.highlight({
    //   gutter: false
    // });
  }
};
</script>
