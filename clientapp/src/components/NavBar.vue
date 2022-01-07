<template>
  <nav
    class="navbar navbar-expand-lg navbar-dark"
    style="font-size: 1.3rem; font-weight: 700"
  >
    <div class="container-fluid container">
      <router-link class="navbar-brand" to="/"> </router-link>
      <button
        class="navbar-toggler"
        type="button"
        data-bs-toggle="collapse"
        data-bs-target="#navbarSupportedContent"
        aria-controls="navbarSupportedContent"
        aria-expanded="false"
        aria-label="Toggle navigation"
      >
        <span class="navbar-toggler-icon" />
      </button>
      <div id="navbarSupportedContent" class="collapse navbar-collapse">
        <ul class="navbar-nav me-auto mb-2 mb-lg-0">
          <li class="nav-item">
            <router-link class="nav-link" aria-current="page" to="/">
              Users
            </router-link>
          </li>
          <li class="nav-item">
            <router-link class="nav-link" to="/playlists">
              PlayLists
            </router-link>
          </li>
          <li class="nav-item">
            <router-link class="nav-link" to=""> Tracks </router-link>
          </li>
          <li class="nav-item dropdown">
            <a
              id="navbarDropdown"
              class="nav-link dropdown-toggle"
              href="#"
              role="button"
              data-bs-toggle="dropdown"
              aria-expanded="false"
            >
              Dictionary
            </a>
            <ul class="dropdown-menu" aria-labelledby="navbarDropdown">
              <li>
                <router-link to="/genres" class="dropdown-item" href="#">
                  Genres
                </router-link>
              </li>
              <li>
                <router-link to="/authors" class="dropdown-item" href="#"
                  >Authors</router-link
                >
              </li>
            </ul>
          </li>
        </ul>
        <!-- <form class="d-flex">
          <input
            class="form-control me-2"
            type="search"
            placeholder="Search"
            aria-label="Search"
          />
          <button class="btn btn-outline-success" type="submit">Search</button>
        </form> -->

        <ul class="navbar-nav ms-auto mb-2 mb-lg-0">
          <li class="nav-item">
            <router-link class="nav-link" to=""> Edit Profile </router-link>
          </li>
          <li class="nav-item">
            <router-link class="nav-link" to=""> LogOut </router-link>
          </li>
        </ul>
      </div>
    </div>
  </nav>
</template>

<script lang="ts">
import { defineComponent } from "vue";

export default defineComponent({});
</script>

<style scoped>
.navbar {
  background: rgb(0, 0, 0);
  background: linear-gradient(
    135deg,
    rgba(0, 0, 0, 1) 0%,
    rgba(250, 81, 110, 1) 52%,
    rgba(210, 49, 120, 1) 100%
  );
}

.navbar-brand {
  background-image: url("https://cdn.discordapp.com/attachments/929061747170635826/929124385787346994/6610336475_926baa46-4849-4458-9cca-6cabe321c151.png");
  width: 200px;
  height: 77px;
  background-repeat: no-repeat;
  background-size: cover;
  stroke: white;
}
</style>
