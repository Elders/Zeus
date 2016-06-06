# Zeus

Zeus is portable CLI tool for machine monitoring built on top of .net core.
## Releases

### [V1.0.0](https://github.com/Elders/Zeus/releases)
Zeus is built with dotnet core 1.0 rc-2, so the targeted runtimes are pretty specific, however it should be possible to run a distribution on older versions of specified operating system.
The windows version requires .Net 4.5+ to be installed on the host OS.

|Operating System| 
|   :---:      |      
| [Ubuntu 15.04-x64]()          | 
| [Debian 8.2-x64](https://github.com/Elders/Zeus/releases/download/1.0.0/debian.8.2-x64.zip)            | 
| [CentOS 7-x64](https://github.com/Elders/Zeus/releases/download/1.0.0/centos.7-x64.zip)              |
| [Windows 7 -x86 or above](https://github.com/Elders/Zeus/releases/download/1.0.0/win-x86.zip)   |
| [Windows 7 -x64 or above](https://github.com/Elders/Zeus/releases/download/1.0.0/ubuntu.15.04-x64.zip)   | 

## Usage

#### Machine

Outputs the system CPU,RAM and HDD

> $ zeus machine

```
{"cpu":{"usage":10.0},"drives":{"c":{"free":18095.0,"total":111939.0,"occupied":93844.0,"usage":83.83494581870483030936492197},"d":{"free":504512.0,"total":937264.0,"occupied":432752.0,"usage":46.171836323597193533518837810},"total":{"free":522607.0,"total":1049203.0,"occupied":526596.0,"usage":50.190096673379698685573716430}},"ram":{"free":6471.0,"total":16326.0,"occupied":9855.0,"usage":60.363836824696802646085997790},"success":true,"error":false}
```
##### Formatting the output
- -f
- --format

By defaul the output its not formated becouse its most like that a machine will read the output. However if youdlike you can add -f or --format to get formated JSON

> $ zeus machine -f

```
{
  "cpu": {
    "usage": 13.0
  },
  "drives": {
    "c": {
      "free": 18094.0,
      "total": 111939.0,
      "occupied": 93845.0,
      "usage": 83.83583916240095051769267190
    },
    "d": {
      "free": 504512.0,
      "total": 937264.0,
      "occupied": 432752.0,
      "usage": 46.171836323597193533518837810
    },
    "total": {
      "free": 522606.0,
      "total": 1049203.0,
      "occupied": 526597.0,
      "usage": 50.190191983820099637534395160
    }
  },
  "ram": {
    "free": 6617.0,
    "total": 16326.0,
    "occupied": 9709.0,
    "usage": 59.469557760627220384662501530
  },
  "success": true,
  "error": false
}
```

##### Assertions

- -e
- --expect

You can also make assertions on the system state like **path:expectaion:value**

**zeus machine -e drives.c.free:above:20000** Will create an assertion that the free space on drive c will be above 20000MB. Having less than 20000MB free space will cause the program to exit with code 1

> zeus machine -e drives.c.free:above:20000 -f

```
{                                                          
  "cpu": {                                                 
    "usage": 11.0                                          
  },                                                       
  "drives": {                                              
    "c": {                                                 
      "free": 18154.0,                                     
      "total": 111939.0,                                   
      "occupied": 93785.0,                                 
      "usage": 83.78223854063373801802767579               
    },                                                     
    "d": {                                                 
      "free": 504512.0,                                    
      "total": 937264.0,                                   
      "occupied": 432752.0,                                
      "usage": 46.171836323597193533518837810              
    },                                                     
    "total": {                                             
      "free": 522666.0,                                    
      "total": 1049203.0,                                  
      "occupied": 526537.0,                                
      "usage": 50.184473357396042519893671670              
    }                                                      
  },                                                       
  "ram": {                                                 
    "free": 6221.0,                                        
    "total": 16326.0,                                      
    "occupied": 10105.0,                                   
    "usage": 61.895136591939238025235820160                
  },                                                       
  "success": false,                                        
  "error": true,                                           
  "errors": [                                              
    "Expectation 'drives.c.free:above:20000' failed"       
  ]                                                        
}                                                          
```

##### Help

> $ zeus --help

```
Zeus 1.0.0
Copyright (C) 2016 Elders

  machine    Performs a check on the pc

  help       Display more information on a specific command.

  version    Display version information.
```

> $ zeus machine --help

```
Zeus 1.0.0
Copyright (C) 2016 Elders

  -e, --expect    Expectations/Assertions about the system status.

  -f, --format    Formats Output

  --help          Display this help screen.

  --version       Display version information.
```

### Consul
The idea behind Zeus is to be used alongisde with [HashiCorps Consul](https://www.consul.io/). Thats why we have provided prebuilt container with [Zeus+Consul](https://github.com/Elders/Consul.Zeus-docker)
