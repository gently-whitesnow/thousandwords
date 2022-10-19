# Перекладываем статику
rm -rf /usr/share/nginx/html/*
mkdir -p /usr/share/nginx/html
cp -rf ~/build/* /usr/share/nginx/html
# Перекладываем конфигурационный файл
rm -rf /etc/nginx/*
mkdir -p /etc/nginx
cp -rf ~/prod/nginx.conf /etc/nginx

# логинимся в докере
docker login https://index.docker.io/v2 -u $DOCKER_LOGIN -p $DOCKER_PWD 

# Запускаем сервис
docker-compose up --force-recreate --build -d
docker image prune -f



# sudo find / -name "*sshd_config*"
# sudo chown -R ci:cicd /etc/nginx/ 