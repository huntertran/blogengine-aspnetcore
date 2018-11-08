<template>
  <v-app dark>
    <v-navigation-drawer app fixed :width="drawerWidth" v-model="isDrawerOpen" clipped floating>
      <v-list dense>
        <template v-for="menu in menus">
          <v-list-tile :key="menu.text" v-on:click="menuClicked(menu)" ripple>
            <v-list-tile-action>
              <v-icon>{{menu.icon}}</v-icon>
            </v-list-tile-action>
            <v-list-tile-content>
              <v-list-tile-title>{{menu.text}}</v-list-tile-title>
            </v-list-tile-content>
          </v-list-tile>
        </template>
      </v-list>
    </v-navigation-drawer>

    <v-toolbar app dense clipped-left>
      <v-toolbar-side-icon class="white--text" @click="isDrawerOpen = !isDrawerOpen"></v-toolbar-side-icon>
      <v-toolbar-title>Blog Engine Admin</v-toolbar-title>
      <v-spacer></v-spacer>
      <v-toolbar-items class="hidden-sm-and-down">
        <v-btn flat>
          <router-link to="./../About">About</router-link>
        </v-btn>
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
export default {
  name: "Layout",
  data: () => ({
    isDrawerOpen: false,
    drawerWidth: 250,
    menus: [
      { icon: "dashboard", text: "Home", url: "/" },
      { icon: "edit", text: "Posts", url: "/Post" }
    ]
  }),
  methods: {
    menuClicked(menu) {
      if (menu.url.length) {
        this.$router.push(menu.url);
      }
    }
  }
};
</script>
