<template>
  <div class="col-md-4 mt-5">
    <div
      @click="setActive"
      class="elevation-4 cover-img me-3 ms-3 rounded selectable"
      :style="`background-image: url(${favorites.picture})`"
    >
      <div class="text-white p-2 d-flex justify-content-between">
        <div>
          <p class="blur rounded-pill px-3 border border-white border-1">
            {{ favorites.category }}
          </p>
        </div>
        <div class="">
          <i
            class="
              mdi mdi-cards-heart-outline
              text-danger
              fs-3
              heart-blur
              rounded-bottom
              p-0
            "
          ></i>
        </div>
        <!-- <div @click="favoriteRecipe" class="">
          <i
            class="
              mdi mdi-heart-outline
              text-white
              fs-3
              heart-blur
              rounded-bottom
              p-0
            "
          ></i>
        </div> -->
      </div>
      <div class="details-blur bottom mx-3 rounded">
        <div class="fs-5 text-white">
          <b>{{ favorites.title }}</b>
        </div>
        <div class="text-white">{{ favorites.subtitle }}</div>
      </div>
    </div>
  </div>
</template>


<script>
import { computed } from '@vue/reactivity'
import { AppState } from '../AppState'
import { recipesService } from '../services/RecipesService'
import { Modal } from 'bootstrap'
import { logger } from '../utils/Logger'
import Pop from '../utils/Pop'
export default {
  props: { favorites: { type: Object, required: true } },
  setup(props) {
    return {
      async setActive() {
        try {
          await recipesService.getById(props.favorites.id)
          Modal.getOrCreateInstance(document.getElementById("recipe-modal")).show();
        } catch (error) {
          Pop.toast(error.message)
          logger.log(error)
        }
      },

    }
  }
}
</script>


<style lang="scss" scoped>
.cover-img {
  height: 400px;
  background-position: center;

  background-size: cover;
}

.blur {
  background-color: rgba(126, 126, 126, 0.6);
  -webkit-backdrop-filter: blur(5px);
  backdrop-filter: blur(5px);
  padding: 5px;
  width: fit-content;
  font-weight: bold;
}
.heart-blur {
  background-color: rgba(126, 126, 126, 0.6);
  -webkit-backdrop-filter: blur(5px);
  backdrop-filter: blur(5px);
  position: relative;
  bottom: 0.4em;
  font-weight: bold;
}

.details-blur {
  background-color: rgba(126, 126, 126, 0.6);
  -webkit-backdrop-filter: blur(5px);
  backdrop-filter: blur(5px);
  padding: 5px;
  border: 0.1px solid white;
  font-weight: bold;
}
.heart-blur:hover {
  transform: scale(1.1);
  transition: 300ms;
  border: solid 2px red;
}

.cover-img:hover {
  transform: scale(1.05);
  transition: 600ms;
}

.bottom {
  margin-top: 16.3rem;
}
</style>