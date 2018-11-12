<template>
  <v-container>

    <v-data-table
    :headers="headers"
    :items="posts"
    hide-actions
    class="elevation-1">
    <template slot="items" slot-scope="props">
      <td>{{ props.item.id }}</td>
      <td>{{ props.item.name }}</td>
      <td>
        <v-btn small flat color="success" v-on:click="showEditPostDialog(props.item.id)">Edit</v-btn>
        <v-btn small flat color="error" v-on:click="showConfirmation(props.item.id)">Delete</v-btn>
      </td>
    </template>
  </v-data-table>

  <v-dialog
      v-model="showDeleteDialog"
      max-width="290"
    >
      <v-card>
        <v-card-title class="headline">Delete post?</v-card-title>

        <v-card-actions>
          <v-spacer></v-spacer>

          <v-btn
            color="green darken-1"
            flat="flat"
            @click="postIdToDelete = 0;showDeleteDialog = false"
          >
            Cancel
          </v-btn>

          <v-btn
            color="green darken-1"
            flat="flat"
            @click="deletePost(postIdToDelete)"
          >
            Delete
          </v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>

  <v-dialog v-model="edit.show" max-width="290">
    <v-card>
      <v-card-title class="headline">Edit Post</v-card-title>

      <v-card-text>
        <v-container grid-list-md>
          <v-layout wrap>
            <v-flex xs12>
                <v-text-field v-model="edit.post.name" label="Post Name" required></v-text-field>
              </v-flex>
          </v-layout>
        </v-container>
      </v-card-text>

      <v-card-actions>
          <v-spacer></v-spacer>

          <v-btn
            color="green darken-1"
            flat="flat"
            @click="edit.show = false"
          >
            Cancel
          </v-btn>

          <v-btn
            color="green darken-1"
            flat="flat"
            @click="editPost(edit.post)"
          >
            Save
          </v-btn>
        </v-card-actions>
    </v-card>
  </v-dialog>

  <v-snackbar v-model="snackbar" :right="true" :bottom="true" :timeout="500">
      {{ message }}
      <v-btn
        color="pink"
        flat
        @click="snackbar = false"
      >
        Close
      </v-btn>
    </v-snackbar>

  </v-container>
  
</template>

<script>
import axios from "axios";

export default {
  data: function() {
    return {
      snackbar: false,
      message: "",
      showDeleteDialog: false,
      postIdToDelete: 0,
      edit: {
        show: false,
        post: {
          id: 0,
          name: ""
        }
      },
      headers: [
        { text: "No.", align: "left", value: "id" },
        { text: "Name", align: "left", value: "name" },
        { text: "Action", align: "left", value: "action", sortable: false }
      ],
      posts: []
    };
  },
  methods: {
    getSinglePostFromArray: function(id) {
      var _this = this;
      var filteredItems = _this.posts.filter(function(element) {
        return element.id === id;
      });

      return filteredItems[0].name;
    },
    getAllPosts: function() {
      var _this = this;
      axios.get("/posts/all").then(function(response) {
        _this.posts = response.data;
      });
    },
    showConfirmation: function(id) {
      var _this = this;
      _this.showDeleteDialog = true;
      _this.postIdToDelete = id;
    },
    deletePost: function(id) {
      var _this = this;
      axios.delete("/posts/delete?id=" + id).then(function(response) {
        if (response.status === 200) {
          _this.message = "Selected post deleted";
          _this.snackbar = true;
          _this.showDeleteDialog = false;
          _this.getAllPosts();
        }
      });
    },
    showEditPostDialog: function(id) {
      var _this = this;
      _this.edit.post.id = id;
      _this.edit.post.name = _this.getSinglePostFromArray(id);
      _this.edit.show = true;
    },
    editPost: function(post) {
      var _this = this;
      axios.put("/posts/put", post).then(function(response) {
        if (response.status === 200) {
          _this.edit.show = false;
          _this.getAllPosts();
        }
      });
    }
  },
  mounted() {
    this.getAllPosts();
  }
};
</script>
