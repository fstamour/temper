#! /usr/bin/env gforth

needs ../avr.fs
needs ../attiny13.fs

\ Set r16 to 1
r16 1 ldi,

\ Set PB0 direction as output
ddrb r16 out,

\ Set the output PB0 high
portb r16 out,

\ Set the output PB0 low
portb r17 out,

sleep,

s" simple-output.bin" write-assembly

free-assembly
bye