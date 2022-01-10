import store from "@/store";
import { createRouter, createWebHistory, RouteRecordRaw } from "vue-router";
import Home from "../views/Home.vue";

const routes: Array<RouteRecordRaw> = [
  {
    path: "/",
    name: "Home",
    component: Home,
  },
  {
    path: "/login",
    name: "Login",
    component: () => import("../views/Login.vue"),
  },
  {
    path: "/registration",
    name: "Registration",
    component: () => import("../views/Register.vue"),
  },
  {
    path: "/reset-password",
    name: "ResetPassword",
    component: () => import("../views/ForgotPassword.vue"),
  },
  {
    path: "/genres",
    name: "Genres",
    component: () => import("../views/Genres.vue"),
  },
  {
    path: "/authors",
    name: "Authors",
    component: () => import("../views/Authors.vue"),
  },
  {
    path: "/playlists/",
    name: "PlayLists",
    component: () => import("../views/PlayLists.vue"),
  },
  {
    path: "/playlist/:id",
    name: "PlayList",
    component: () => import("../views/PlayList.vue"),
  },
  {
    path: "/tracks",
    name: "Tracks",
    component: () => import("../views/Tracks.vue"),
  },
];

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes,
});

router.beforeEach((to, from, next) => {
  const user = localStorage.getItem("user");

  if (user && to.name !== "Login" && to.name !== "Registration") {
    store.commit("changeAuthorize", true);
    next();
  } else if (user && (to.name === "Login" || to.name === "Registration")) {
    next({ name: "Home" });
  } else if (to.name == "Login") {
    store.commit("changeAuthorize", false);
    next();
  } else {
    store.commit("changeAuthorize", false);
    next({ name: "Login" });
  }
});

export default router;
