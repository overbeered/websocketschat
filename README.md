# websocketschat

### first of all
```
git clone https://github.com/overbeered/websocketschat.git
```
### then build docker container
```
docker build -f Dockerfile -t wsschat ..
```
### and then run this container
```
docker run -d -p 7777:80 wsschat:latest 
```
