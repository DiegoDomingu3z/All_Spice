import{B as d,c as l,a as t,o as a,b as s,d as p,F as r,z as f,e as v,C as _}from"./vendor.4f835de4.js";import{_ as m,f as u,l as g,P as F,A as B}from"./index.caf57bfc.js";const k={setup(){return d(async()=>{try{await u.getFavorites()}catch(e){g.log(e),F.toast(e.message)}}),{favorites:l(()=>B.myFavorites)}}},x={class:"row mb-5"};function y(e,b,h,c,M,P){const n=t("Favorites"),i=t("RecipeDetailedModal");return a(),s(r,null,[p("div",x,[(a(!0),s(r,null,f(c.favorites,o=>(a(),_(n,{key:o.id,favorites:o},null,8,["favorites"]))),128))]),v(i)],64)}var w=m(k,[["render",y]]);export{w as default};