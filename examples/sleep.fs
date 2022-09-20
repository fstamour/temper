#! /usr/bin/env gforth

include ../avr.fs

10 0assembly
sleep,
s" sleep.bin" write-assembly
free-assembly

bye