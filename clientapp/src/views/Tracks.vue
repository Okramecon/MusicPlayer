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
      <tbody>
        <tr
          v-for="track in tracks"
          :key="track.id"
          :style="track.id == currentIndex ? '' : ''"
          :class="track.id == currentIndex ? '' : ''"
        >
          <th scope="row">{{ track.id }}</th>
          <td>cover</td>
          <td>{{ track.name }}</td>
          <td>123</td>
          <td>123</td>
          <td>
            <button
              type="button"
              class="btn me-2"
              @click="selectSound(track.id)"
            >
              <svg
                v-if="state.audioPlaying[track.id]"
                @click="play()"
                class="w-6 h-6 m-auto"
                fill="currentColor"
                xmlns="http://www.w3.org/2000/svg"
                viewBox="0 0 20 20"
              >
                <path d="M5 4h3v12H5V4zm7 0h3v12h-3V4z" />
              </svg>
              <svg
                v-else
                @click="pause()"
                class="w-6 h-6 m-auto"
                xmlns="http://www.w3.org/2000/svg"
                fill="none"
                viewBox="0 0 24 24"
                stroke="currentColor"
              >
                <path
                  stroke-linecap="round"
                  stroke-linejoin="round"
                  stroke-width="2"
                  d="M14.752 11.168l-3.197-2.132A1 1 0 0010 9.87v4.263a1 1 0 001.555.832l3.197-2.132a1 1 0 000-1.664z"
                />
                <path
                  stroke-linecap="round"
                  stroke-linejoin="round"
                  stroke-width="2"
                  d="M21 12a9 9 0 11-18 0 9 9 0 0118 0z"
                />
              </svg>
            </button>
            <button
              type="button"
              class="btn me-2"
              data-bs-toggle="modal"
              data-bs-target="#exampleModal"
              @click="prepareForAction(track), (currentAction = 'Edit')"
            >
              Edit
            </button>
            <button
              type="button"
              class="btn ms-2"
              data-bs-toggle="modal"
              data-bs-target="#exampleModal"
              @click="prepareForAction(track), (currentAction = 'Delete')"
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
          {{ currentAction }}
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

    <!-- Player -->
    <div class="mt-auto" style="margin-inline: -8vw">
      <div class="controls">
        <div class="d-grid" style="grid-template-columns: 1fr 1fr">
          <img
            class="w-14 h-14 rounded-full m-auto"
            src="https://tailwindcss.com/img/card-top.jpg"
          />
          <div class="font-semibold m-auto" style="color: white">
            <p>{{ currentTrackrForm.name }}</p>
            <p class="text-xs">{{ currentTrackrForm.text }}</p>
          </div>
        </div>
        <div class="d-block">
          <div class="flex">
            <div class="flex w-50 m-auto justify-between items-center my-2">
              <div class="text-grey-darker hover:bg-gray-300 rounded-full p-1">
                <svg
                  @click="controlsButtons.random = !controlsButtons.random"
                  :class="controlsButtons.random ? 'text-red-500' : ''"
                  class="w-6 h-6 cursor-pointer"
                  fill="currentColor"
                  xmlns="http://www.w3.org/2000/svg"
                  viewBox="0 0 20 20"
                >
                  <path
                    d="M6.59 12.83L4.4 15c-.58.58-1.59 1-2.4 1H0v-2h2c.29 0 .8-.2 1-.41l2.17-2.18 1.42 1.42zM16 4V1l4 4-4 4V6h-2c-.29 0-.8.2-1 .41l-2.17 2.18L9.4 7.17 11.6 5c.58-.58 1.59-1 2.41-1h2zm0 10v-3l4 4-4 4v-3h-2c-.82 0-1.83-.42-2.41-1l-8.6-8.59C2.8 6.21 2.3 6 2 6H0V4h2c.82 0 1.83.42 2.41 1l8.6 8.59c.2.2.7.41.99.41h2z"
                  />
                </svg>
              </div>
              <div class="text-grey-darker hover:bg-gray-300 rounded-full p-1">
                <svg
                  @click="controlsButtons.prevButton ? previous() : ''"
                  class="w-6 h-6 cursor-pointer"
                  fill="currentColor"
                  xmlns="http://www.w3.org/2000/svg"
                  viewBox="0 0 20 20"
                >
                  <path d="M4 5h3v10H4V5zm12 0v10l-9-5 9-5z" />
                </svg>
              </div>
              <div
                class="text-white p-3 rounded-full bg-gradient-to-r from-red-500 via-red-600 to-red-700 shadow-lg"
              >
                <svg
                  v-if="!pauseTrack"
                  @click="play()"
                  class="w-8 h-8 cursor-pointer"
                  xmlns="http://www.w3.org/2000/svg"
                  fill="none"
                  viewBox="0 0 24 24"
                  stroke="currentColor"
                >
                  <path
                    stroke-linecap="round"
                    stroke-linejoin="round"
                    stroke-width="2"
                    d="M14.752 11.168l-3.197-2.132A1 1 0 0010 9.87v4.263a1 1 0 001.555.832l3.197-2.132a1 1 0 000-1.664z"
                  />
                  <path
                    stroke-linecap="round"
                    stroke-linejoin="round"
                    stroke-width="2"
                    d="M21 12a9 9 0 11-18 0 9 9 0 0118 0z"
                  />
                </svg>
                <svg
                  v-else
                  @click="pause()"
                  class="w-8 h-8 cursor-pointer"
                  fill="currentColor"
                  xmlns="http://www.w3.org/2000/svg"
                  viewBox="0 0 20 20"
                >
                  <path d="M5 4h3v12H5V4zm7 0h3v12h-3V4z" />
                </svg>
              </div>
              <div class="text-grey-darker hover:bg-gray-300 rounded-full p-1">
                <svg
                  @click="controlsButtons.nextButton ? next() : ''"
                  class="w-6 h-6 cursor-pointer"
                  fill="currentColor"
                  xmlns="http://www.w3.org/2000/svg"
                  viewBox="0 0 20 20"
                >
                  <path d="M13 5h3v10h-3V5zM4 5l9 5-9 5V5z" />
                </svg>
              </div>
              <div class="text-grey-darker hover:bg-gray-300 rounded-full p-1">
                <svg
                  @click="controlsButtons.repeat = !controlsButtons.repeat"
                  :class="controlsButtons.repeat ? 'text-red-500' : ''"
                  class="w-6 h-6 cursor-pointer"
                  fill="currentColor"
                  xmlns="http://www.w3.org/2000/svg"
                  viewBox="0 0 20 20"
                >
                  <path
                    d="M5 4a2 2 0 0 0-2 2v6H0l4 4 4-4H5V6h7l2-2H5zm10 4h-3l4-4 4 4h-3v6a2 2 0 0 1-2 2H6l2-2h7V8z"
                  />
                </svg>
              </div>
            </div>
          </div>
          <div class="d-flex w-75 m-auto">
            <div
              class="text-sm text-grey-darker w-2/12 md:w-1/12 font-semibold"
            >
              <p>{{ timer }}</p>
            </div>
            <div class="mt-2 relative w-8/12 md:w-10/12">
              <div
                @click="seek($event)"
                ref="progress"
                class="h-1 bg-grey-dark cursor-pointer rounded-full bg-gray-500"
              >
                <div
                  class="flex w-full justify-end h-1 bg-gradient-to-r from-red-500 to-red-700 rounded-full relative"
                  :style="{ width: step + '%' }"
                ></div>
              </div>
              <div
                class="flex w-full justify-end h-1 rounded-full relative"
                :style="{ width: step + '%' }"
              >
                <span
                  id="progressButtonTimer"
                  class="w-3 h-3 md:w-4 md:h-4 bg-gradient-to-r from-red-500 to-red-700 absolute pin-r pin-b -mb-1 rounded-full shadow"
                ></span>
              </div>
            </div>
            <div
              class="text-sm text-grey-darker w-2/12 md:w-1/12 font-semibold"
            >
              <p>{{ duration }}</p>
            </div>
          </div>
        </div>
        <div class="d-grid" style="grid-template-columns: 0.3fr 0.7fr">
          <div class="hover:bg-gray-500 rounded-full m-auto" @click="mute()">
            <svg
              v-if="mutePlayer"
              class="w-6 h-6 m-auto text-red-500 cursor-pointer"
              xmlns="http://www.w3.org/2000/svg"
              fill="none"
              viewBox="0 0 24 24"
              stroke="currentColor"
            >
              <path
                stroke-linecap="round"
                stroke-linejoin="round"
                stroke-width="2"
                d="M5.586 15H4a1 1 0 01-1-1v-4a1 1 0 011-1h1.586l4.707-4.707C10.923 3.663 12 4.109 12 5v14c0 .891-1.077 1.337-1.707.707L5.586 15z"
                clip-rule="evenodd"
              />
              <path
                stroke-linecap="round"
                stroke-linejoin="round"
                stroke-width="2"
                d="M17 14l2-2m0 0l2-2m-2 2l-2-2m2 2l2 2"
              />
            </svg>
            <svg
              v-else
              class="w-6 h-6 m-auto cursor-pointer"
              xmlns="http://www.w3.org/2000/svg"
              fill="none"
              viewBox="0 0 24 24"
              stroke="currentColor"
            >
              <path
                stroke-linecap="round"
                stroke-linejoin="round"
                stroke-width="2"
                d="M15.536 8.464a5 5 0 010 7.072m2.828-9.9a9 9 0 010 12.728M5.586 15H4a1 1 0 01-1-1v-4a1 1 0 011-1h1.586l4.707-4.707C10.923 3.663 12 4.109 12 5v14c0 .891-1.077 1.337-1.707.707L5.586 15z"
              />
            </svg>
          </div>
          <div class="w-75 m-auto relative">
            <div
              @click="volume($event)"
              ref="volBar"
              class="h-1 bg-grey-dark cursor-pointer rounded-full bg-gray-500 m-auto relative"
              style="width: 100%"
            >
              <div
                class="flex w-full justify-end h-1 bg-gradient-to-r from-red-500 to-red-700 rounded-full relative"
                :style="{ width: volumeProgress + '%' }"
              ></div>
            </div>
            <div
              class="flex justify-end h-1 rounded-full relative"
              :style="{ width: volumeProgress + '%' }"
            >
              <span
                id="progressButtonVolume"
                class="w-3 h-3 md:w-4 md:h-4 bg-gradient-to-r from-red-500 to-red-700 absolute pin-r pin-b -mb-1 rounded-full shadow"
              ></span>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import {
  GetAllTracks,
  CreateTrack,
  EditTrack,
  DeleteTrack,
  GetAllAuthors,
} from "@/api";
import { defineComponent, reactive, ref } from "vue";
import Multiselect from "@vueform/multiselect";
import { Howl, Howler } from "howler";

export default defineComponent({
  components: {
    Multiselect,
  },
  mounted() {
    const barWidth = 0.9;
    this.sliderBtnVol =
      this.volBar.offsetWidth * barWidth + this.volBar.offsetWidth * 0.05 - 25;
  },
  async setup() {
    const step = ref(0);
    const controlsButtons = reactive({
      prevButton: true,
      nextButton: true,
      random: false,
      repeat: false,
    });

    const currentIndex = ref(0);
    const duration = ref("00:00");
    const timer = ref("00:00");
    const pauseTrack = ref(false);
    const progress = ref(null);
    const volBar = ref(null);
    const sliderBtn = ref(0);
    const sliderBtnVol = ref(null);
    const volumeProgress = ref(90);
    const mutePlayer = ref(false);
    const tracks = ref(await GetAllTracks());
    const state = reactive({
      audioPlaying: [],
    });

    const closeButton = ref();
    const upload = new FormData();
    const currentTrackrForm = reactive({
      id: 0,
      name: "",
      summary: "",
      text: "",
      coverUrl: "",
      upload: {},
      musicUrl: "",
      authorId: 0,
      howl: null,
    });
    const currentAction = ref("Create");
    const authors = ref(await GetAllAuthors());

    const options = ref([]);

    authors.value.forEach((x) => {
      options.value.push({
        value: x.id,
        name: x.fullName,
      });
    });

    const previewFiles = async (event) => {
      const file = event.target.files[0];
      upload.append("file", file);
    };

    const actionTrack = async () => {
      if (currentTrackrForm.name) {
        switch (currentAction.value) {
          case "Create":
            {
              upload.append("name", currentTrackrForm.name);
              upload.append("summary", currentTrackrForm.summary);
              upload.append("text", currentTrackrForm.text);
              upload.append("authorId", currentTrackrForm.authorId.toString());
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

    const prepareForAction = (track) => {
      currentTrackrForm.id = track.id;
      currentTrackrForm.name = track.name;
      currentTrackrForm.summary = track.summary;
      currentTrackrForm.text = track.text;
      currentTrackrForm.coverUrl = track.coverUrl;
      currentTrackrForm.authorId = track.authorId;
      currentTrackrForm.upload = upload;
      currentTrackrForm.howl = track.howl;
    };

    // Functions
    const formatTime = (secs) => {
      const minutes = Math.floor(secs / 60) || 0;
      const seconds = Math.floor(secs - minutes * 60) || 0;
      return (
        (minutes < 10 ? "0" : "") +
        minutes +
        ":" +
        (seconds < 10 ? "0" : "") +
        seconds
      );
    };

    const play = () => {
      let sound;

      const track = tracks.value.find((t) => t.id == currentIndex.value);
      if (track.howl) {
        sound = track.howl;
      } else {
        state.audioPlaying[currentIndex.value] = false;
        sound = track.howl = new Howl({
          src: [track.musicUrl],
          html5: true, // A live stream can only be played through HTML5 Audio.
          format: ["mp3", "aac"],
          onplay: () => {
            pauseTrack.value = true;
            controlsButtons.nextButton = true;
            controlsButtons.prevButton = true;
            duration.value = formatTime(sound.duration());
            requestAnimationFrame(stepFunction.bind(this));
          },
          onpause: () => (pauseTrack.value = false),
          onend: () => next(),
          onseek: () => window.requestAnimationFrame(stepFunction.bind(this)),
        });
      }

      sound.play();
      state.audioPlaying[currentIndex.value] = true;
    };

    const pause = () => {
      const track = tracks.value.find((t) => t.id == currentIndex.value);
      if (track.howl) {
        track.howl.pause();
        pauseTrack.value = false;
        state.audioPlaying[currentIndex.value] = false;
      }
    };

    const stepFunction = () => {
      const track = tracks.value.find((t) => t.id == currentIndex.value);
      const seek = track.howl.seek();
      timer.value = formatTime(Math.round(seek));
      step.value = (seek * 100) / track.howl.duration();

      sliderBtn.value =
        progress.value.offsetWidth * (step.value / 100) +
        progress.value.offsetWidth * 0.05 -
        25;

      if (track.howl.playing())
        window.requestAnimationFrame(stepFunction.bind(this));
    };

    const seek = (event) => {
      const per = event.offsetX / progress.value.clientWidth;

      const track = tracks.value.find((t) => t.id == currentIndex.value);

      if (track.howl) {
        let barWidth = 0;
        if (track.howl.playing()) {
          track.howl.pause();
          track.howl.seek(track.howl.duration() * per);
          barWidth = (per * 100) / 100;
          sliderBtn.value =
            progress.value.offsetWidth * barWidth +
            progress.value.offsetWidth * 0.05 -
            25;
          track.howl.play();
        } else {
          track.howl.seek(track.howl.duration() * per);
          barWidth = (per * 100) / 100;
          sliderBtn.value =
            progress.value.offsetWidth * barWidth +
            progress.value.offsetWidth * 0.05 -
            25;
        }
      }
    };

    const next = () => {
      controlsButtons.nextButton = false;
      const track = tracks.value.find((t) => t.id == currentIndex.value);

      state.audioPlaying[currentIndex.value] = false;
      mutePlayer.value ? (mutePlayer.value = false) : "";
      track.howl && track.howl.mute(true) ? track.howl.mute(false) : "";

      if (track.howl && tracks.value.length - 1 == currentIndex.value) {
        track.howl.stop();
        controlsButtons.repeat
          ? currentIndex.value
          : controlsButtons.random
          ? (currentIndex.value = Math.floor(
              Math.random() * tracks.value.length
            ))
          : (currentIndex.value = 0);
      } else {
        if (track.howl) {
          track.howl.stop();
        }
        controlsButtons.repeat
          ? currentIndex.value
          : controlsButtons.random
          ? (currentIndex.value = Math.floor(
              Math.random() * tracks.value.length
            ))
          : currentIndex.value++;
      }

      play();
    };

    const previous = () => {
      const track = tracks.value.find((t) => t.id == currentIndex.value);
      controlsButtons.prevButton = false;
      state.audioPlaying[currentIndex.value] = false;
      mutePlayer.value ? (mutePlayer.value = false) : "";
      track.howl && track.howl.mute(true) ? track.howl.mute(false) : "";
      if (!track.howl) {
        currentIndex.value = tracks.value.length - 1;
      } else if (track.howl && currentIndex.value == 0) {
        track.howl.stop();
        controlsButtons.repeat
          ? currentIndex.value
          : controlsButtons.random
          ? (currentIndex.value = Math.floor(
              Math.random() * tracks.value.length
            ))
          : (currentIndex.value = tracks.value.length - 1);
      } else if (track.howl) {
        track.howl.stop();

        controlsButtons.repeat
          ? currentIndex.value
          : controlsButtons.random
          ? (currentIndex.value = Math.floor(
              Math.random() * tracks.value.length
            ))
          : currentIndex.value--;
      }

      play();
    };

    const selectSound = (selectedId) => {
      console.log(selectedId);
      const track = tracks.value.find((x) => x.id == selectedId);
      currentTrackrForm.id = track.id;
      currentTrackrForm.name = track.name;
      currentTrackrForm.text = track.text;
      currentTrackrForm.summary = track.summary;
      // currentTrackrForm.musicUrl = track.musicUrl;
      // currentTrackrForm.authorId = track.authorId;
      // currentTrackrForm.howl = track.howl;
      // currentTrackrForm.coverUrl = track.coverUrl;
      // currentTrackrForm.upload = track.upload;

      if (track.howl) {
        track.howl.stop();
        state.audioPlaying[currentIndex.value] = false;
      }
      currentIndex.value = selectedId;

      play();
    };

    const volume = (event) => {
      const per = event.layerX / parseFloat(volBar.value.scrollWidth);
      const barWidth = (per * 100) / 100;
      volumeProgress.value = barWidth * 100;
      sliderBtnVol.value =
        volBar.value.offsetWidth * barWidth +
        volBar.value.offsetWidth * 0.05 -
        25;
      Howler.volume(per);
    };

    const mute = () => {
      const track = tracks.value.find((t) => t.id == currentIndex.value);

      if (track.howl) {
        mutePlayer.value = !mutePlayer.value;

        mutePlayer.value ? track.howl.mute(true) : track.howl.mute(false);
      }
    };

    return {
      closeButton,
      prepareForAction,
      currentAction,
      actionTrack,
      tracks,
      currentTrackrForm,
      options,
      previewFiles,
      play,
      pause,
      duration,
      formatTime,
      pauseTrack,
      next,
      previous,
      currentIndex,
      timer,
      step,
      stepFunction,
      seek,
      selectSound,
      state,
      controlsButtons,
      progress,
      volume,
      volBar,
      volumeProgress,
      sliderBtn,
      mute,
      mutePlayer,
      sliderBtnVol,
    };
  },
});
</script>

<style src="@vueform/multiselect/themes/default.css"></style>
<style scoped>
.controls {
  background: rgb(0, 0, 0);
  background: linear-gradient(
    135deg,
    rgba(0, 0, 0, 1) 0%,
    rgba(250, 81, 110, 1) 52%,
    rgba(210, 49, 120, 1) 100%
  );
  box-shadow: 0px 0px 7px 10px rgba(59, 87, 195, 0.39);
  margin-inline: 40px;
  border-radius: 50px;
  display: grid;
  grid-template-columns: 0.3fr 1.4fr 0.3fr;
}
#journal-scroll::-webkit-scrollbar {
  width: 4px;
  cursor: pointer;
}
#journal-scroll::-webkit-scrollbar-track {
  background-color: rgba(229, 231, 235, var(--bg-opacity));
  cursor: pointer;
}
#journal-scroll::-webkit-scrollbar-thumb {
  cursor: pointer;
  background-color: #a0aec0;
}
.cd-center {
  position: absolute;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);
  width: 60px;
  height: 60px;
  border-radius: 50%;
  background-color: #f7fafc;
}
#progressButtonTimer,
#progressButtonVolume {
  margin-top: -9px;
  right: -8px;
}
@media screen and (max-width: 768px) {
  #progressButtonTimer,
  #progressButtonVolume {
    margin-top: -8px;
    right: -7px;
  }
}
th,
td {
  width: 13%;
}

th:last-child,
td:last-child {
  width: 17%;
}
</style>
