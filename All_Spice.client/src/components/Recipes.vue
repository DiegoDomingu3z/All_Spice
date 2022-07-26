<template>
  <div class="col-md-4 mt-5">
    <div
      @click="setActive"
      class="elevation-4 cover-img me-3 ms-3 rounded selectable"
      :style="`background-image: url(${recipe.picture})`"
    >
      <div class="text-white p-2 d-flex justify-content-between">
        <div>
          <p class="blur rounded-pill px-3 border border-white border-1">
            {{ recipe.category }}
          </p>
        </div>
        <div v-if="recipe.creatorId != account.id">
          <div v-if="isFavorite" class="">
            <i
              @click.stop="deleteFavorite(favorite.id)"
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
          <div v-else @click.stop="favoriteRecipe" class="">
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
          </div>
        </div>
      </div>
      <div class="details-blur bottom mx-3 rounded">
        <div class="fs-5 text-white">
          <b>{{ recipe.title }}</b>
        </div>
        <div class="text-white">{{ recipe.subtitle }}</div>
      </div>
    </div>
  </div>
</template>


<script>
import { computed } from '@vue/reactivity'
import { accountService } from '../services/AccountService'
import { logger } from '../utils/Logger'
import Pop from '../utils/Pop'
import { AppState } from '../AppState'
import { favoritesService } from '../services/FavoriteService'
import { recipesService } from '../services/RecipesService'
import { Modal } from 'bootstrap'

export default {
  props: { recipe: { type: Object, required: true } },
  setup(props) {
    return {

      async favoriteRecipe() {
        try {
          await favoritesService.favoriteRecipe({ recipeId: props.recipe.id, accountId: AppState.account.id })
          Pop.toast("Favorite")
        } catch (error) {
          Pop.toast(error.message)
          logger.log(error)
        }
      },
      async setActive() {
        try {
          await recipesService.getById(props.recipe.id)
          Modal.getOrCreateInstance(
            document.getElementById("recipe-modal")
          ).show();
        } catch (error) {
          Pop.toast(error.message)
          logger.log(error)
        }
      },
      async deleteFavorite(id) {
        try {
          await favoritesService.deleteFavorite(id)
        } catch (error) {
          logger.log(error)
          Pop.toast(error.message)
        }
      },
      account: computed(() => AppState.account),
      favorite: computed(() => AppState.myFavorites),
      recipes: computed(() => AppState.recipes),
      isFavorite: computed(() => !!AppState.myFavorites.find(f => f.id == props.recipe.id))



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