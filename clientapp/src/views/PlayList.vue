<template>
  <div class="about">
    <h1>{{ playlist.name }}</h1>
  </div>
  <hr />
  <table class="table">
    <thead>
      <tr>
        <th scope="col">#</th>
        <th scope="col">Cover</th>
        <th scope="col">Name</th>
        <th scope="col">Summary</th>
        <th scope="col">Text</th>
        <th scope="col">Actions</th>
      </tr>
    </thead>
    <tbody>
      <tr v-for="track in playlist.tracks" :key="track.id">
        <th scope="row">{{ track.id }}</th>
        <td>cover</td>
        <td>{{ track.name }}</td>
        <td>123</td>
        <td>123</td>
        <td>
          <button
            type="button"
            class="btn ms-2"
            @click="removeTrackFromPlaylist(track.id)"
          >
            Delete
          </button>
        </td>
      </tr>
    </tbody>
  </table>

  <!-- Button trigger modal -->
  <button
    type="button"
    class="btn"
    data-bs-toggle="modal"
    data-bs-target="#exampleModal"
  >
    Add Track
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
            {{ currentAction }} Track
          </h5>
        </div>
        {{ currentAction }}
        <div v-if="currentAction != 'Delete'" class="modal-body">
          <label for="genreInput" class="form-label">Author</label>
          <Multiselect
            v-model="selectedTrackId"
            placeholder="Select your character"
            label="name"
            :options="options"
            @select="selectMusic"
          />
          <audio class="w-100 mt-2" controls id="audio"></audio>
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
            @click="addTrackToPlaylist"
            class="block-button btn welcome"
          >
            <span> {{ currentAction }} </span>
          </button>
        </div>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import {
  AddTrackToPlaylist,
  GetAllTracks,
  GetPlaylist,
  RemoveTrackFromPlaylist,
} from "@/api";
import Playlist from "@/models/PlayList";
import { defineComponent, ref } from "vue";
import { useRoute } from "vue-router";
import Multiselect from "@vueform/multiselect";

export default defineComponent({
  components: {
    Multiselect,
  },
  async setup() {
    const closeButton = ref();
    const route = useRoute();
    const id = ref(parseInt(route.params.id[0]));
    const playlist = ref<Playlist>(await GetPlaylist(id.value));
    const tracks = ref(await GetAllTracks());
    const options = ref<{ value: number; name: string }[]>([]);
    tracks.value.forEach((x) => {
      options.value.push({
        value: x.id,
        name: x.name,
      });
    });

    const selectedTrackId = ref(0);
    const currentAction = ref("Add");

    const selectMusic = () => {
      const audio = document.getElementById("audio") as HTMLMediaElement;
      const track = tracks.value.find((x) => x.id == selectedTrackId.value);
      if (track) audio.src = track.musicUrl as string;
    };

    const removeTrackFromPlaylist = async (trackId: number) => {
      const removedTrackId = await RemoveTrackFromPlaylist(id.value, trackId);
      if (removedTrackId)
        playlist.value.tracks.splice(
          playlist.value.tracks.findIndex((x) => x.id == trackId),
          1
        );
      closeButton.value.click();
    };

    const addTrackToPlaylist = async () => {
      const track = await AddTrackToPlaylist(id.value, selectedTrackId.value);
      if (track.id) playlist.value.tracks.push(track);
      closeButton.value.click();
    };

    return {
      playlist,
      options,
      selectedTrackId,
      tracks,
      currentAction,
      selectMusic,
      addTrackToPlaylist,
      removeTrackFromPlaylist,
      closeButton,
    };
  },
});
</script>

<style src="@vueform/multiselect/themes/default.css"></style>
