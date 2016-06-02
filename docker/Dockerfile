FROM microsoft/dotnet:core-deps

ADD /build/unix /var/app

RUN chmod -R 777 /var/app

RUN apt-get update \
    && apt-get install -y --no-install-recommends \ 
          wget \ 
          unzip

ADD /consul /var/consul 

RUN ls -l /var/consul && tail -n 100 /var/consul/config/client.json

RUN ip -o -4 addr list

RUN chmod -R 777 /var/consul

RUN wget https://releases.hashicorp.com/consul/0.6.4/consul_0.6.4_linux_amd64.zip --no-check-certificate

RUN unzip consul_0.6.4_linux_amd64.zip -d /var/consul
    
# Server RPC is used for communication between Consul clients and servers for internal
# request forwarding.
EXPOSE 8300

# Serf LAN and WAN (WAN is used only by Consul servers) are used for gossip between
# Consul agents. LAN is within the datacenter and WAN is between just the Consul
# servers in all datacenters.
EXPOSE 8301 8301/udp 8302 8302/udp

# CLI, HTTP, and DNS (both TCP and UDP) are the primary interfaces that applications
# use to interact with Consul.
EXPOSE 8400 8500 8600 8600/udp


WORKDIR /var/

#ENTRYPOINT ["bash","./consul/entrypoint.sh","agent"]
ENTRYPOINT ["/var/app/Zeus"]


