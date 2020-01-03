<template>
  <v-container>
    <v-tabs v-model="active"
            color="cyan"
            dark
            slider-color="yellow">

      <v-tab :key="0" ripple>
        <h2>Published</h2>
      </v-tab>

      <v-tab-item :key="0">
        <v-data-table
          :headers="headers"
          :items="posts"
          hide-default-footer
          class="elevation-1">
          <template slot="items" slot-scope="props">
            <td>{{ props.item.id }}</td>
            <td>{{ props.item.title }}</td>
            <td>{{ props.item.summary }}</td>
            <td>{{ props.item.editedDateTime }}</td>
            <td>
              <v-checkbox disabled v-model="props.item.isPublished"></v-checkbox>
            </td>
            <td>
              <v-btn small flat color="success" v-on:click="showEditPost(props.item.id)">Edit</v-btn>
              <v-btn small flat color="error" v-on:click="showConfirmation(props.item.id)">Delete</v-btn>
            </td>
          </template>          
        </v-data-table>
        <div class="text-xs-center">
          <v-pagination
            v-model="postPaging.page"
            :length="postPageLen"
            @input="getNextPosts()"
          ></v-pagination>
        </div>
      </v-tab-item>

      <v-tab :key="1" ripple>
        <h2>Drafts</h2>
      </v-tab>

      <v-tab-item :key="1">
        <v-data-table
          :headers="headers"
          :items="drafts"
          hide-actions
          class="elevation-1">
          <template slot="items" slot-scope="props">
            <td>{{ props.item.id }}</td>
            <td>{{ props.item.title }}</td>
            <td>{{ props.item.summary }}</td>
            <td>{{ props.item.editedDateTime }}</td>
            <td>
              <v-checkbox disabled v-model="props.item.isPublished"></v-checkbox>
            </td>
            <td>
              <v-btn small flat color="success" v-on:click="showEditPost(props.item.id)">Edit</v-btn>
              <v-btn small flat color="error" v-on:click="showConfirmation(props.item.id)">Delete</v-btn>
            </td>
          </template>
          <div class="text-xs-center">
          <v-pagination
            v-model="draftPaging.page"
            :length="draftPageLen"
            @input="getNextDrafts()"
          ></v-pagination>
          </div>
        </v-data-table>        
      </v-tab-item>
      
    </v-tabs>



  <v-dialog v-model="showDeleteDialog"
            max-width="290">
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
import axios from 'axios';

export default {
  data: function() {
    return {
      active: 'draft',
      snackbar: false,
      message: '',
      showDeleteDialog: false,
      postIdToDelete: 0,
      edit: {
        show: false,
        post: {
          id: 0,
          name: ''
        }
      },
      headers: [
        { text: 'No.', align: 'left', value: 'id' },
        { text: 'Title', align: 'left', value: 'title' },
        { text: 'Summary', align: 'left', value: 'summary' },
        {
          text: 'Edited Time',
          align: 'left',
          value: 'editedDateTime'
        },
        { text: 'Is Published', align: 'left', value: 'isPublished' },
        { text: 'Action', align: 'left', value: 'action', sortable: false }
      ],
      posts: [],
      postPaging: {
        page: 0,
        itemPerPage: 5,
        totalItems: 0
      },
      drafts: [],
      draftPaging: {
        page: 0,
        itemPerPage: 5,
        totalItems: 0
      }
    };
  },
  computed: {
    postPageLen: function() {
      return Math.ceil(
        this.postPaging.totalItems / this.postPaging.itemPerPage
      );
    },
    draftPageLen: function() {
      return Math.ceil(
        this.draftPaging.totalItems / this.draftPaging.itemPerPage
      );
    }
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
      axios.get('/posts/all').then(function(response) {
        _this.posts = response.data;
      });
      axios.get('/posts/GetTotalPostNumber').then(function(response) {
        _this.postPaging.totalItems = response.data;
      });
    },
    getNextPosts: function() {
      var _this = this;
      axios
        .get(
          '/posts/all?page=' +
            _this.postPaging.page +
            '&postPerPage=' +
            _this.postPaging.itemPerPage
        )
        .then(function(response) {
          _this.posts = response.data;
        });
    },
    getAllDrafts: function() {
      var _this = this;
      axios.get('/posts/GetUnpublishedPosts').then(function(response) {
        _this.drafts = response.data;
      });
    },
    getNextDrafts: function() {
      var _this = this;
      axios
        .get(
          '/posts/GetUnpublishedPosts?page=' +
            _this.draftPaging.page +
            '&postPerPage=' +
            _this.draftPaging.itemPerPage
        )
        .then(function(response) {
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
      axios.delete('/posts/delete?id=' + id).then(function(response) {
        if (response.status === 200) {
          _this.message = 'Selected post deleted';
          _this.snackbar = true;
          _this.showDeleteDialog = false;
          _this.getAllPosts();
        }
      });
    },
    showEditPost: function(id) {
      var _this = this;
      _this.$router.push('/admin/posts/edit/' + id);
    },
    editPost: function(post) {
      var _this = this;
      axios.put('/posts/put', post).then(function(response) {
        if (response.status === 200) {
          _this.edit.show = false;
          _this.getAllPosts();
        }
      });
    }
  },
  mounted() {
    this.getAllPosts();
    this.getAllDrafts();
  }
};
</script>
