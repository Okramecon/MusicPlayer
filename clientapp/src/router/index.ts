import { createRouter, createWebHistory, RouteRecordRaw } from "vue-router";
import Home from "../views/Home.vue";

const routes: Array<RouteRecordRaw> = [
  {
    path: "/",
    name: "Home",
    component: Home,
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
    path: "/playlists",
    name: "PlayLists",
    component: () => import("../views/PlayLists.vue"),
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

export default router;
