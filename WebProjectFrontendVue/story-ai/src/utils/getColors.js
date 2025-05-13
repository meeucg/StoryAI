import { Vibrant } from 'node-vibrant/browser';

export async function getPalette(url) {
    return Vibrant.from(url).getPalette()
}
