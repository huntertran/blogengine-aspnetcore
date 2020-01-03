<template>
    <v-app id="inspire">
        <div v-if="isShowAdminLayout">
            <v-navigation-drawer v-model="drawer" app clipped>
            <v-list dense>
                <template v-for="menu in menus">
                    <v-list-item
                        v-if="!menu.children"
                        :key="menu.text"
                        v-on:click="menuClicked(menu)"
                    >
                        <v-list-item-action>
                            <v-icon>{{menu.icon}}</v-icon>
                        </v-list-item-action>
                        <v-list-item-content>
                            <v-list-item-title>{{menu.text}}</v-list-item-title>
                        </v-list-item-content>
                    </v-list-item>
                    <v-list-group v-else :key="menu.text" :prepend-icon="menu.icon" ripple>
                        <template slot="activator">
                            <v-list-item-content>
                                <v-list-item-title>{{ menu.text }}</v-list-item-title>
                            </v-list-item-content>
                        </template>
                        <v-list-item
                            v-for="subMenu in menu.children"
                            :key="subMenu.text"
                            @click="menuClicked(subMenu)"
                        >
                            <v-list-item-action>
                                <v-icon>{{ subMenu.icon }}</v-icon>
                            </v-list-item-action>
                            <v-list-item-content>
                                <v-list-item-title>{{ subMenu.text }}</v-list-item-title>
                            </v-list-item-content>
                        </v-list-item>
                    </v-list-group>
                </template>
            </v-list>
            </v-navigation-drawer>

            <v-app-bar clipped-left>
            <v-app-bar-nav-icon @click.stop="drawer = !drawer" />
            <v-toolbar-title v-on:click="goHome">Hunter's Blog</v-toolbar-title>
            </v-app-bar>

            <v-content>
                <v-container fluid>
                    <router-view></router-view>
                </v-container>
            </v-content>
        </div>
        
        <div v-else>
            <v-toolbar dense>
                <v-toolbar-title v-on:click="goHome">Hunter's Blog</v-toolbar-title>
                <v-spacer></v-spacer>
                <v-toolbar-items class="hidden-sm-and-down">
                    <v-btn text :key="category.id"
                        v-for="category in categories"
                        v-on:click="categoryClicked(category.id)">{{category.name}}</v-btn>
                </v-toolbar-items>
            </v-toolbar>
        <router-view :key="$route.fullPath"></router-view>
        </div>

        <v-footer app>
            <span>&copy; {{year}}</span>
        </v-footer>
    </v-app>
</template>

<script>
import Axios from "axios";

export default {
    data: () => ({
        year: 2019,
        drawer: null,
        isDrawerOpen: true,
        drawerWidth: 250,
        categories: [],
        menus: [
            {
                icon: "mdi-view-dashboard",
                text: "Home",
                url: "/admin"
            },
            {
                icon: "mdi-pencil",
                text: "Posts",
                isOpen: false,
                children: [
                    { text: "All Posts", url: "/admin/posts" },
                    { text: "Add New", url: "/admin/posts/add" }
                ]
            },
            {
                icon: "mdi-shape-outline",
                text: "Categories",
                isOpen: false,
                children: [
                    { text: "All Categories", url: "/admin/categories" },
                    { text: "Add New", url: "/admin/categories/add" }
                ]
            }
        ],
        topButtons: [
            {
                icon: "info",
                text: "About",
                name: "/about"
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
    props: {
        source: String
    },
    computed: {
        isLoggedIn: function() {
            return this.$store.getters.isLoggedIn;
        },
        isShowAdminLayout: function() {
            if (this.isLoggedIn && this.$route.path.startsWith("/admin")) {
                // console.log(this.$route);
                return true;
            } else {
                return false;
            }
        }
    },
    methods: {
        getCategories: function() {
            var _this = this;
            Axios.get("/categories/all").then(function(response) {
                _this.categories = response.data;
            });
        },
        menuClicked(menu) {
            if (menu.url.length) {
                this.$router.push(menu.url);
            }
        },
        goHome() {
            this.$router.push("/");
        },
        categoryClicked(categoryId) {
            this.$router.push(
                "/?page=1&postPerPage=5&categoryId=" + categoryId
            );
        },
        login() {
            this.$router.push("/admin/login");
        },
        logout() {
            this.$store.dispatch("logout").then(() => {
                this.$router.push("/");
            });
        }
    },
    mounted() {
        this.getCategories();
    },
    created: function() {
        this.$http.interceptors.response.use(undefined, function(err) {
            // return new Promise(function(resolve, reject) {
            //   if (err.status === 401 && err.config && !err.config.__isRetryRequest) {
            //     this.$store.dispatch("logout");
            //   }
            //   throw err;
            // });
            return new Promise(function() {
                if (
                    err.status === 401 &&
                    err.config &&
                    !err.config.__isRetryRequest
                ) {
                    this.$store.dispatch("logout");
                }
                throw err;
            });
        });
    }
};
</script>
