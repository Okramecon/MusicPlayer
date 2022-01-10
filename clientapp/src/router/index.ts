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

// router.beforeEach((to, from, next) => {
//   console.log(localStorage.getItem("user"));
//   if (!localStorage.getItem("user")) {
//     next("/login");
//   } else {
//     next();
//   }
// });

export default router;
