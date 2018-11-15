<template>
    <v-container>
        <v-layout>
            <v-flex xs12>
                <v-content>
                    {{post.content}}
                </v-content>
            </v-flex>
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
    this.getPost(16);
  }
};
</script>

