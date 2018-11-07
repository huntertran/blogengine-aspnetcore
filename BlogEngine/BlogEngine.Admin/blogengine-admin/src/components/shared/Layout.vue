<template>
  <v-app dark>
    <v-navigation-drawer app fixed :width="drawerWidth" v-model="isDrawerOpen" clipped floating>

    </v-navigation-drawer>

    <v-toolbar app dense clipped-left>
      <v-toolbar-side-icon class="white--text" @click="isDrawerOpen = !isDrawerOpen"></v-toolbar-side-icon>
      <v-toolbar-title>Blog Engine Admin</v-toolbar-title>
      <v-spacer></v-spacer>
      <v-toolbar-items class="hidden-sm-and-down">
        <v-btn flat>About</v-btn>
        <v-btn flat>Sign out</v-btn>
      </v-toolbar-items>
    </v-toolbar>

    <v-content>
      <v-container fluid>
        <router-view></router-view>
      </v-container>
    </v-content>

    <v-footer app></v-footer>
  </v-app>
</template>

<script>
// import HelloWorld from "../HelloWorld";

export default {
  name: "Layout",
  components: {
    // HelloWorld
  },
  data() {
    return {
      isDrawerOpen: false
    };
  }
};
</script>
