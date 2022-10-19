# Перекладываем статику
sudo rm -rf /usr/share/nginx/html/*
sudo mkdir -p /usr/share/nginx/html
sudo cp -rf ~/build/* /usr/share/nginx/html
# Перекладываем конфигурационный файл
sudo rm -rf /etc/nginx/*
sudo mkdir -p /etc/nginx
sudo cp -rf ~/prod/nginx.conf /etc/nginx

# логинимся в докере
docker login -u $DOCKER_LOGIN -p $DOCKER_PWD

# Запускаем сервис
docker-compose up --force-recreate --build -d
docker image prune -f
