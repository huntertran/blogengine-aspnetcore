import Vue from "vue";
import Router from "vue-router";

import About from "./components/About";
import Home from "./components/Home";

import Login from "./components/Login";
import Secure from "./components/Secure";

import store from "./store.js";

// Categories
import AllCategories from "./components/categories/AllCategories";
import AddCategory from "./components/categories/AddCategory";

// Posts
import AllPosts from "./components/posts/AllPosts";
import AddPost from "./components/posts/AddPost";
import EditPost from "./components/posts/EditPost";

Vue.use(Router);

const router = new Router({
  mode: "history",
  routes: [
    {
      path: "/admin/login",
      name: "login",
      component: Login
    },
    {
      path: "/admin/secure",
      name: "secure",
      component: Secure,
      meta: {
        requiresAuth: true
      }
    },
    {
      path: "/",
      name: "home",
      component: Home
    },
    {
      path: "/about",
      name: "about",
      component: About
    },
    {
      path: "/admin/categories",
      name: "categories",
      component: AllCategories
    },
    {
      path: "/admin/categories/add",
      name: "addcategory",
      component: AddCategory
    },
    {
      path: "/admin/posts",
      name: "posts",
      component: AllPosts
    },
    {
      path: "/admin/posts/add",
      name: "addpost",
      component: AddPost
    },
    {
      path: "/admin/posts/edit/:id",
      props: true,
      name: "editpost",
      component: EditPost
    }
  ]
});

// handling unauthorized access
router.beforeEach((to, from, next) => {
  if (to.matched.some(record => record.meta.requiresAuth)) {
    if (store.getters.isLoggedIn) {
      next();
      return;
    }
    next("/admin/login");
  } else {
    next();
  }
});

export default router;
