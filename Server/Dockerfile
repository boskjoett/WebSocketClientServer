FROM microsoft/dotnet-framework:4.7.2-runtime

WORKDIR /app

# Copy binary output files to WORKDIR in image
COPY bin/Release/* ./

# Expose websocket port
EXPOSE 80

ENTRYPOINT Server.exe

# Run this command in a command prompt window to build the image:
# docker build -t socketserver .

# Run the image in a container with this command
# docker run -it -p 80:80 --rm socketserver
