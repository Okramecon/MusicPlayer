<template>
  <div class="h-100 d-flex flex-column">
    <div class="about">
      <h1>Tracks</h1>
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
    </table>

    <!-- Button trigger modal -->
    <button
      type="button"
      class="btn btn-primary"
      data-bs-toggle="modal"
      data-bs-target="#exampleModal"
      @click="currentAction = 'Create'"
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
          <div v-if="currentAction != 'Delete'" class="modal-body">
            <label for="genreInput" class="form-label">Name</label>
            <input
              v-model="currentTrackrForm.name"
              type="text"
              class="form-control"
              id="genreInput"
              aria-describedby="genreHelp"
            />
            <label for="genreInput" class="form-label">Summary</label>
            <input
              v-model="currentTrackrForm.summary"
              type="text"
              class="form-control"
              id="genreInput"
              aria-describedby="genreHelp"
            />
            <label for="genreInput" class="form-label">Text</label>
            <input
              v-model="currentTrackrForm.text"
              type="text"
              class="form-control"
              id="genreInput"
              aria-describedby="genreHelp"
            />
            <label for="genreInput" class="form-label">Author</label>
            <Multiselect
              v-model="currentTrackrForm.authorId"
              placeholder="Select your character"
              label="name"
              :options="options"
            />

            <label for="file" class="form-label">Track file</label>
            <input
              class="form-control"
              aria-describedby="genreHelp"
              type="file"
              id="file"
              name="file"
              @change="previewFiles"
            />

            <label for="genreInput" class="form-label">Cover</label>
            <input
              v-model="currentTrackrForm.coverUrl"
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
              @click="actionTrack()"
              class="block-button btn welcome"
            >
              <span v-if="currentAction == 'Delete'"> Yes </span>
              <span v-else> {{ currentAction }} </span>
            </button>
          </div>
        </div>
      </div>
    </div>
    <Player />
  </div>
</template>

<script lang="ts">
import {
  GetAllTracks,
  CreateTrack,
  EditTrack,
  DeleteTrack,
  GetAllAuthors,
} from "@/api";
import Author from "@/models/Author";
import Track from "@/models/Track";
import { defineComponent, reactive, ref } from "vue";
import Multiselect from "@vueform/multiselect";
import Player from "@/components/Player.vue";

export default defineComponent({
  components: {
    Multiselect,
    Player,
  },
  async setup() {
    const closeButton = ref();
    const upload = new FormData();
    const currentTrackrForm = reactive<Track>({
      id: 0,
      name: "",
      summary: "",
      text: "",
      coverUrl: "",
      upload: new Blob(),
      authorId: 0,
    });
    const currentAction = ref("Create");
    const tracks = ref<Track[]>(await GetAllTracks());
    const authors = ref<Author[]>(await GetAllAuthors());

    const options = ref<{ value: number; name: string }[]>([]);

    authors.value.forEach((x) => {
      options.value.push({
        value: x.id,
        name: x.fullName,
      });
    });

    const previewFiles = async (event: any) => {
      const file = event.target.files[0];
      upload.append("name", currentTrackrForm.name);
      upload.append("file", file);
      upload.append("summary", currentTrackrForm.summary);
      upload.append("text", currentTrackrForm.text);
      upload.append("authorId", currentTrackrForm.authorId.toString());
    };

    const actionTrack = async () => {
      if (currentTrackrForm.name) {
        switch (currentAction.value) {
          case "Create":
            {
              const track = await CreateTrack(upload);
              if (track.id) tracks.value.push(track);
            }
            closeButton.value.click();
            break;

          case "Edit": {
            const index = tracks.value.findIndex(
              (g) => g.id == currentTrackrForm.id
            );
            if (~index) {
              const editedTrack = await EditTrack(currentTrackrForm);

              tracks.value.splice(index, 1, editedTrack);
            }
            closeButton.value.click();
            break;
          }
          default: {
            const index = tracks.value.findIndex(
              (g) => g.id == currentTrackrForm.id
            );
            if (~index) {
              const id = await DeleteTrack(currentTrackrForm.id);
              if (id) {
                tracks.value.splice(index, 1);
              }
            }
            closeButton.value.click();
            break;
          }
        }
      } else alert("Enter track name");
    };

    // const prepareForAction = (track: Track) => {
    //   currentTrackrForm.id = track.id;
    //   currentTrackrForm.name = track.name;
    //   currentTrackrForm.summary = track.summary;
    //   currentTrackrForm.text = track.text;
    //   currentTrackrForm.coverUrl = track.coverUrl;
    //   currentTrackrForm.upload = track;
    //   currentTrackrForm.authorId = track.authorId;
    // };

    return {
      closeButton,
      // prepareForAction,
      currentAction,
      actionTrack,
      tracks,
      currentTrackrForm,
      options,
      previewFiles,
    };
  },
});
</script>

<style src="@vueform/multiselect/themes/default.css"></style>
<style scoped>
th,
td {
  width: 13%;
}

th:last-child,
td:last-child {
  width: 17%;
}
</style>
