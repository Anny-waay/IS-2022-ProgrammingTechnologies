cmake_minimum_required(VERSION 3.21)
project(sum)

set(CMAKE_CXX_STANDARD 14)

add_library(sum SHARED library.cpp)
