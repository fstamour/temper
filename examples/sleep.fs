#! /usr/bin/env gforth

needs ../avr.fs
needs ../attiny13.fs


sleep,

s" sleep.bin" write-assembly

free-assembly
bye