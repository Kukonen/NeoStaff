# Use an official Node runtime as a parent image
FROM node:19-alpine as build

# Set the working directory to /app
WORKDIR /app

# Copy the package.json and package-lock.json to the container
COPY package*.json ./

# Install dependencies
RUN npm install
RUN npm install react-router-dom --save

# Copy the rest of the application code to the container
COPY . .

# Build the React app
RUN npm run build

CMD ["npm", "start"]