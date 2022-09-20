# temper

An 8bits AVR assembler written in gforth

## Useful commands

### Convert from bin to hex format

```sh
avr-objcopy -I binary -O ihex a.bin a.hex
```

### Disassemble a hex file

```sh
avr-objdump -D a.hex -m avr25
```

### Simulate a MCU

```sh
simavr --mcu attiny13 -f 9600000 a.hex
```
