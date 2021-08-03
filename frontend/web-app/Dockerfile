FROM node:lts-alpine as build-stage
WORKDIR /app
# COPY package*.json ./
# RUN npm install
COPY ./dist ./dist
# RUN npm run build -- --mode staging

FROM nginx as production-stage
RUN mkdir /app
COPY --from=build-stage /app/dist /app
COPY nginx.conf /etc/nginx/nginx.conf