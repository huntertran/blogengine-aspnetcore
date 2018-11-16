import Vue from "vue";
import Router from "vue-router";

import About from "./components/About";
import Home from "./components/Home";

import Secure from "./components/Secure";

import store from "./store.js";

// Admin
import Admin from "./components/Admin";
import AdminHome from "./components/admin/AdminHome";
import AdminLogin from "./components/admin/Login";

// Categories
import AllCategories from "./components/categories/AllCategories";
import AddCategory from "./components/categories/AddCategory";

// Posts
import AllPosts from "./components/posts/AllPosts";
import AddPost from "./components/posts/AddPost";
import EditPost from "./components/posts/EditPost";

// Read post
import ReadPost from "./components/posts/ReadPost";

Vue.use(Router);

const router = new Router({
  mode: "history",
  routes: [
    {
      path: "/admin",
      component: Admin,
      children: [
        {
          path: "",
          name: "admin",
          component: AdminHome
        },
        {
          path: "login",
          component: AdminLogin
        },
        {
          path: "secure",
          component: Secure,
          meta: {
            requiresAuth: true
          }
        },
        {
          path: "categories",
          name: "categories",
          component: AllCategories
        },
        {
          path: "categories/add",
          name: "addcategory",
          component: AddCategory
        },
        {
          path: "posts",
          name: "posts",
          component: AllPosts
        },
        {
          path: "posts/add",
          name: "addpost",
          component: AddPost
        },
        {
          path: "posts/edit/:id",
          props: true,
          name: "editpost",
          component: EditPost
        }
      ]
    },
    {
      path: "/",
      name: "home",
      props: true,
      component: Home
    },
    {
      path: "/post/:postId",
      component: ReadPost
    },
    {
      path: "/about",
      name: "about",
      component: About
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
