# Билдим статику
npm run build --prefix ~/thousandwords/frontend # TEST

# Запускаем сервис
docker-compose up --force-recreate --build -d
docker image prune -f
