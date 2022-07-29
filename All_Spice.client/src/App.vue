<template>
  <header class="bg-white">
    <Navbar />
  </header>
  <main class="bg-white">
    <router-view />
  </main>
  <Modal id="recipe">
    <template #header>New Recipe</template>
    <template #body><RecipeForm /></template>
  </Modal>
  <div class="fab">
    <button
      class="btn btn-success rounded-pill f-22 elevation-4"
      type="button"
      data-bs-toggle="modal"
      data-bs-target="#recipe"
    >
      <i class="mdi mdi-plus"></i>
    </button>
  </div>
</template>

<script>
import { computed } from 'vue'
import { AppState } from './AppState'
export default {
  name: 'App',
  setup() {
    return {
      appState: computed(() => AppState)
    }
  }
}
</script>
<style lang="scss">
@import "./assets/scss/main.scss";

.fab {
  position: fixed;
  bottom: 3em;
  right: 1em;
}
</style>
