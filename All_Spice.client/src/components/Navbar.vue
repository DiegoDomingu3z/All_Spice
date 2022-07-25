<template>
  <div class="bg-white">
    <div class="m-3 elevation-5">
      <div
        class="bg rounded"
        style="
          background-image: url(https://images.unsplash.com/photo-1444228425018-ff8535a55c93?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=2942&q=80);
        "
      >
        <div class="col-12 text-end p-2">
          <Login />
        </div>
        <div class="col-12 text-center text-white">
          <h1>All-Spice</h1>
          <h4>Cherish your family and their cooking</h4>
        </div>
        <div class="col-12 d-flex justify-content-center">
          <div class="pages text-center elevation-5 rounded p-3 d-flex">
            <div class="px-5">
              <router-link class="" :to="{ name: 'Home' }">
                <div class="text-success">Home</div>
              </router-link>
            </div>
            <div class="px-5">
              <router-link :to="{ name: 'myRecipes' }"
                ><div class="text-success">My Recipes</div></router-link
              >
            </div>
            <div class="px-5">
              <router-link :to="{ name: 'favorites' }"
                ><div class="text-success">Favorites</div></router-link
              >
            </div>
          </div>
        </div>
        <button
          class="navbar-toggler"
          type="button"
          data-bs-toggle="collapse"
          data-bs-target="#navbarText"
          aria-controls="navbarText"
          aria-expanded="false"
          aria-label="Toggle navigation"
        >
          <span class="navbar-toggler-icon" />
        </button>

        <ul class="navbar-nav me-auto"></ul>
        <!-- LOGIN COMPONENT HERE -->
      </div>
    </div>
  </div>
</template>

<script>
export default {
  setup() {
    return {};
  },
};
</script>

<style>
.bg {
  height: 230px;
  width: 100%;
  background-position: center;
  background-size: cover;
}

.pages {
  background-color: white;
  width: fit-content;
  font-weight: bold;
  transform: translate(0px, 50px);
}
</style>
