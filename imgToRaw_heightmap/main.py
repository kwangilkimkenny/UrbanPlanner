import toraw
import os

infile = 'terrain_jeju.png'
# path_re = os.path.abspath(filePath)
if __name__ == '__main__':
    toraw.convert(infile, "outfile.raw")

    # toraw.convert("terrain_jeju.png", "terrain_jeju_raw")


## 실행방법 ##
# >> python main.py