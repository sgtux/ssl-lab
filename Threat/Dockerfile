FROM ubuntu
ARG DEBIAN_FRONTEND=noninteractive
WORKDIR /root
RUN apt update && \
  apt install -y openssh-server netcat net-tools iputils-ping wget curl vim traceroute dnsutils python3 tcpdump && \
  ssh-keygen -A && \
  mkdir -p /run/sshd && \
  useradd -m -s /bin/bash -p $(openssl passwd -1 123) threat
CMD ["/usr/sbin/sshd", "-D"]