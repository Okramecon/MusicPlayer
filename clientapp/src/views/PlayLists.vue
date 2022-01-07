<template>
  <div class="about">
    <h1>PlayLists</h1>
  </div>
  <hr />
  <div class="grid-play-lists">
    <div
      v-for="playlist in playlists"
      class="block-playlist welcome"
      :key="playlist.id"
    >
      <div
        v-if="playlist.coverUrl"
        class="block-playlist custom-cover"
        :style="{ backgroundImage: `url(${playlist.coverUrl})` }"
      >
        <span class="playlist-name">
          {{ playlist.name }}
        </span>
      </div>
      <div v-else class="block-playlist welcome front-layer">
        <span class="playlist-name">
          {{ playlist.name }}
        </span>
      </div>
    </div>
  </div>

  <button
    type="button"
    class="btn btn-primary"
    data-bs-toggle="modal"
    data-bs-target="#exampleModal"
    @click="currentAction = 'Create'"
  >
    Add genre
  </button>

  <!-- Modal -->
  <div
    class="modal fade"
    id="exampleModal"
    tabindex="-1"
    aria-labelledby="exampleModalLabel"
    aria-hidden="true"
  >
    <div class="modal-dialog">
      <div class="modal-content">
        <div class="modal-header">
          <h5 class="modal-title" id="exampleModalLabel">
            {{ currentAction }} Genre
          </h5>
        </div>
        <div v-if="currentAction != 'Delete'" class="modal-body">
          <label for="genreInput" class="form-label">Playlist name</label>
          <input
            v-model="currentPlaylistForm.name"
            required
            type="text"
            class="form-control"
            id="genreInput"
            aria-describedby="genreHelp"
          />
          <label for="genreInput" class="form-label">Playlist summary</label>
          <textarea
            v-model="currentPlaylistForm.summary"
            type="text"
            class="form-control"
            id="genreInput"
            aria-describedby="genreHelp"
          />
          <label for="genreInput" class="form-label">Playlist cover</label>
          <input
            v-model="currentPlaylistForm.coverUrl"
            type="text"
            class="form-control"
            id="genreInput"
            aria-describedby="genreHelp"
          />
        </div>
        <div class="modal-footer">
          <button
            type="button"
            class="block-button btn welcome"
            data-bs-dismiss="modal"
            ref="closeButton"
          >
            <span> Cancel </span>
          </button>
          <button
            type="button"
            @click="actionGenre()"
            class="block-button btn welcome"
          >
            <span v-if="currentAction == 'Delete'"> Yes </span>
            <span v-else> {{ currentAction }} </span>
          </button>
        </div>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import {
  GetAllPlaylists,
  CreatePlaylist,
  EditPlaylist,
  DeletePlaylist,
} from "@/api";
import Playlist from "@/models/PlayList";
import { defineComponent, reactive, ref } from "vue";

export default defineComponent({
  async setup() {
    const closeButton = ref();
    const currentAction = ref("Create");
    const currentPlaylistForm = reactive({
      id: 0,
      name: "",
      summary: "",
      coverUrl: "",
    });

    const playlists = ref<Playlist[]>(await GetAllPlaylists());

    const actionGenre = async () => {
      if (currentPlaylistForm.name) {
        switch (currentAction.value) {
          case "Create":
            {
              const playlist = await CreatePlaylist(currentPlaylistForm);
              if (playlist.id) playlists.value.push(playlist);
            }
            closeButton.value.click();
            break;

          case "Edit": {
            const index = playlists.value.findIndex(
              (g) => g.id == currentPlaylistForm.id
            );
            if (~index) {
              const copiedPlaylist: Playlist = JSON.parse(
                JSON.stringify(playlists.value[index])
              );
              const editedGenre = await EditPlaylist(copiedPlaylist);

              playlists.value.splice(index, 1, editedGenre);
            }
            closeButton.value.click();
            break;
          }
          default: {
            const index = playlists.value.findIndex(
              (g) => g.id == currentPlaylistForm.id
            );
            if (~index) {
              const id = await DeletePlaylist(currentPlaylistForm.id);
              if (id) {
                playlists.value.splice(index, 1);
              }
            }
            closeButton.value.click();
            break;
          }
        }
      } else alert("Enter playlist name");
    };

    const prepareForAction = (playlist: Playlist) => {
      currentPlaylistForm.id = playlist.id;
      currentPlaylistForm.name = playlist.name;
      currentPlaylistForm.summary = playlist.summary;
      currentPlaylistForm.coverUrl = playlist.coverUrl;
    };

    return {
      currentPlaylistForm,
      playlists,
      actionGenre,
      closeButton,
      prepareForAction,
      currentAction,
    };
  },
});
</script>

<style scoped>
.playlist-name {
  -webkit-text-stroke: 1px black;
  display: flex;
  height: 100%;
  align-items: center;
  justify-content: center;
  font-size: 2rem;
  font-family: inherit;
  font-weight: 600;
}

.custom-cover {
  background-repeat: no-repeat;
  margin: 0;
  background-position: center;
  margin: 0;
}
.custom-cover:hover {
  transform: scale(1) !important;
}
.front-layer {
  margin: 0;
}
.front-layer:hover {
  transform: scale(1) !important;
}
</style>
