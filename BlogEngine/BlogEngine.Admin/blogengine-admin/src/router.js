import Vue from "vue";
import VueRouter from "vue-router";

import About from "./components/About";
import Home from "./components/Home";

import Login from "./components/Login";
import Secure from "./components/Secure";

import store from "./store.js";

// Categories
import AllCategories from "./components/categories/AllCategories";

Vue.use(VueRouter);

const router = new VueRouter({
  mode: "history",
  routes: [
    {
      path: "/login",
      name: "login",
      component: Login
    },
    {
      path: "/secure",
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
      path: "/About",
      name: "about",
      component: About
    },
    {
      path: "/Categories",
      name: "categories",
      component: AllCategories
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
    next("/login");
  } else {
    next();
  }
});

export default router;
