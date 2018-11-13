<template>
  <v-app dark>
    <v-navigation-drawer app :width="drawerWidth" v-model="isDrawerOpen" clipped>
      <v-list dense>
        <template v-for="menu in menus">
          <v-list-tile v-if="!menu.children" :key="menu.text" v-on:click="menuClicked(menu)" ripple>
            <v-list-tile-action>
              <v-icon>{{menu.icon}}</v-icon>
            </v-list-tile-action>
            <v-list-tile-content>
              <v-list-tile-title>{{menu.text}}</v-list-tile-title>
            </v-list-tile-content>
          </v-list-tile>
          <v-list-group v-else :key="menu.text" :prepend-icon="menu.icon" ripple>
            <v-list-tile slot="activator" ripple>
              <v-list-tile-content>
                <v-list-tile-title>{{ menu.text }}</v-list-tile-title>
              </v-list-tile-content>
              </v-list-tile>
              <v-list-tile v-for="subMenu in menu.children" :key="subMenu.text" ripple @click="menuClicked(subMenu)">
                  <v-list-tile-action>
                      <v-icon>{{ subMenu.icon }}</v-icon>
                  </v-list-tile-action>
                  <v-list-tile-content>
                      <v-list-tile-title>{{ subMenu.text }}</v-list-tile-title>
                  </v-list-tile-content>
              </v-list-tile>
          </v-list-group>
        </template>
      </v-list>
      <v-list dense class="hidden-md-and-up">
          <v-list-tile v-for="button in topButtons" :key="button.text" v-on:click="menuClicked(menu)" ripple>
            <v-list-tile-action>
              <v-icon>{{button.icon}}</v-icon>
            </v-list-tile-action>
            <v-list-tile-content>
              <v-list-tile-title>{{button.text}}</v-list-tile-title>
            </v-list-tile-content>
          </v-list-tile>
      </v-list>
    </v-navigation-drawer>

    <v-toolbar app dense clipped-left>
      <v-toolbar-side-icon class="white--text" @click="isDrawerOpen = !isDrawerOpen"></v-toolbar-side-icon>
      <v-toolbar-title>Blog Engine Admin</v-toolbar-title>
      <v-spacer></v-spacer>
      <v-toolbar-items class="hidden-sm-and-down">
        <v-btn flat v-for="button in topButtons" :key="button.text" :to="button.name">{{button.text}}</v-btn>
        <v-btn flat v-if="isLoggedIn" v-on:click="logout">{{authenticateButtons[1].text}}</v-btn>
        <v-btn flat v-else v-on:click="login">{{authenticateButtons[0].text}}</v-btn>
      </v-toolbar-items>
    </v-toolbar>

    <v-content>
      <v-container fluid>
        <router-view></router-view>
      </v-container>
    </v-content>

    <!-- <v-footer app>
      <p>Copy right: 2019 hunter.tran</p>
    </v-footer> -->
  </v-app>
</template>
  
<script>
export default {
  data: () => ({
    isDrawerOpen: false,
    drawerWidth: 250,
    menus: [
      {
        icon: "dashboard",
        text: "Home",
        url: "/"
      },
      {
        icon: "edit",
        text: "Posts",
        isOpen: false,
        children: [
          { text: "All Posts", url: "/posts" },
          { text: "Add New", url: "/posts/add" }
        ]
      },
      {
        icon: "view_module",
        text: "Categories",
        isOpen: false,
        children: [
          { text: "All Categories", url: "/categories" },
          { text: "Add New", url: "/categories/add" }
        ]
      }
    ],
    topButtons: [
      {
        icon: "info",
        text: "About",
        name: "about"
      }
    ],
    authenticateButtons: [
      {
        icon: "portrait",
        text: "Login",
        name: "login"
      },
      {
        icon: "exit_to_app",
        text: "Log out",
        name: "logout"
      }
    ]
  }),
  computed: {
    isLoggedIn: function() {
      return this.$store.getters.isLoggedIn;
    }
  },
  methods: {
    menuClicked(menu) {
      if (menu.url.length) {
        this.$router.push(menu.url);
      }
    },
    login() {
      this.$router.push("/login");
    },
    logout() {
      this.$store.dispatch("logout").then(() => {
        this.$router.push("/login");
      });
    }
  },
  created: function() {
    this.$http.interceptors.response.use(undefined, function(err) {
      return new Promise(function(resolve, reject) {
        if (err.status === 401 && err.config && !err.config.__isRetryRequest) {
          this.$store.dispatch(logout);
        }
        throw err;
      });
    });
  }
};
</script>
