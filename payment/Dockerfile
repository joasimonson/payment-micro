FROM hayd/alpine-deno:1.0.0

EXPOSE 80

RUN mkdir -p /usr/src/app
WORKDIR /usr/src/app
COPY . .

USER deno
RUN deno cache app.ts
CMD ["run", "--allow-env", "--allow-read", "--allow-net", "--unstable", "app.ts"]