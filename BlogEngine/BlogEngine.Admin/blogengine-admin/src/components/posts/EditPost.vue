<template>
  <v-container>
    <v-layout>
      <v-flex xs12>
        <v-form>
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
          <!-- <v-text-field
            v-model="post.content"
            label="Post Content"
            required
          ></v-text-field> -->
          <editor
            api-key="lb0mhaz5el6xk5icuw5ohvya2g0o4on9yj6lqm4bw6zsnl3e"
            v-model="post.content"
          ></editor>
          <v-btn v-on:click="submit">Submit</v-btn>
        </v-form>
      </v-flex>
    </v-layout>
  </v-container>
</template>

<script>
import Axios from "axios";
// es modules
import Editor from "@tinymce/tinymce-vue";

export default {
  props: ["id"],
  data: function() {
    return {
      post: {
        title: "",
        summary: "",
        content: ""
      }
    };
  },
  components: {
    editor: Editor
  },
  methods: {
    submit: function() {
      var _this = this;
      Axios.put("/posts/put/", _this.post).then(function(response) {
        if (response.status === 200 || response.status === 201) {
          _this.$router.push("/posts");
        }
      });
    },
    getPost: function(id) {
      var _this = this;
      Axios.get("/posts/find?id=" + id).then(function(response) {
        _this.post = response.data;
      });
    }
  },
  mounted: function() {
    this.getPost(this.id);
  }
};
</script>
